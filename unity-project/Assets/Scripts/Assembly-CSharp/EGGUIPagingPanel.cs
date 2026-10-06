using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("EG/GUI/Paging Panel")]
public class EGGUIPagingPanel : IgnoreTimeScale
{
	public delegate void OnChangedPage(int paged);

	public delegate void OnDragFinished();

	public GameObject sourceObj;

	public float pageWidth = 640f;

	public float pageHeight = 1136f;

	public int _selectPage;

	public OnChangedPage onChangePage;

	private int pageNum;

	private bool restrictWithinPanel = true;

	private bool disableDragIfFits = true;

	private UIDraggablePanel.DragEffect dragEffect = UIDraggablePanel.DragEffect.MomentumAndSpring;

	private bool smoothDragStart;

	private Vector3 scale = new Vector3(1f, 0f, 0f);

	public float scrollWheelFactor;

	public float momentumAmount = 35f;

	private Vector2 relativePositionOnReset = Vector2.zero;

	public bool repositionClipping;

	public bool iOSDragEmulation = true;

	public OnDragFinished onDragFinished;

	private Transform mTrans;

	private UIPanel mPanel;

	private Plane mPlane;

	private Vector3 mLastPos;

	private bool mPressed;

	private Vector3 mMomentum = Vector3.zero;

	private float mScroll;

	private Bounds mBounds;

	private bool mCalculatedBounds;

	private bool mShouldMove;

	private bool mIgnoreCallbacks;

	private int mDragID = -10;

	private Vector2 mDragStartOffset = Vector2.zero;

	private bool mDragStarted;

	public UIPanel panel
	{
		get
		{
			return mPanel;
		}
	}

	public int PageSelected
	{
		get
		{
			return _selectPage;
		}
		private set
		{
			_selectPage = value;
		}
	}

	public Bounds bounds
	{
		get
		{
			if (!mCalculatedBounds)
			{
				mCalculatedBounds = true;
				mBounds = CalculatePagesBounds(mTrans);
			}
			return mBounds;
		}
	}

	public bool shouldMoveHorizontally
	{
		get
		{
			float num = bounds.size.x;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += mPanel.clipSoftness.x * 2f;
			}
			return num > mPanel.clipRange.z;
		}
	}

	public bool shouldMoveVertically
	{
		get
		{
			float num = bounds.size.y;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += mPanel.clipSoftness.y * 2f;
			}
			return num > mPanel.clipRange.w;
		}
	}

	private bool shouldMove
	{
		get
		{
			if (!disableDragIfFits)
			{
				return true;
			}
			if (mPanel == null)
			{
				mPanel = GetComponent<UIPanel>();
			}
			Vector4 clipRange = mPanel.clipRange;
			Bounds bounds = this.bounds;
			float num = ((clipRange.z != 0f) ? (clipRange.z * 0.5f) : ((float)Screen.width));
			float num2 = ((clipRange.w != 0f) ? (clipRange.w * 0.5f) : ((float)Screen.height));
			if (!Mathf.Approximately(scale.x, 0f))
			{
				if (bounds.min.x < clipRange.x - num)
				{
					return true;
				}
				if (bounds.max.x > clipRange.x + num)
				{
					return true;
				}
			}
			if (!Mathf.Approximately(scale.y, 0f))
			{
				if (bounds.min.y < clipRange.y - num2)
				{
					return true;
				}
				if (bounds.max.y > clipRange.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public Vector3 currentMomentum
	{
		get
		{
			return mMomentum;
		}
		set
		{
			mMomentum = value;
			mShouldMove = true;
		}
	}

	private Bounds CalculatePagesBounds(Transform root)
	{
		EGGUIPage[] componentsInChildren = root.GetComponentsInChildren<EGGUIPage>();
		pageNum = componentsInChildren.Length;
		if (pageNum == 0)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		EGGUIPage eGGUIPage = componentsInChildren[0];
		EGGUIPage eGGUIPage2 = componentsInChildren[pageNum - 1];
		EGGUIPage[] array = componentsInChildren;
		EGGUIPage[] array2 = array;
		foreach (EGGUIPage eGGUIPage3 in array2)
		{
			if (eGGUIPage.transform.localPosition.x > eGGUIPage3.transform.localPosition.x)
			{
				eGGUIPage = eGGUIPage3;
			}
			if (eGGUIPage2.transform.localPosition.x < eGGUIPage3.transform.localPosition.x)
			{
				eGGUIPage2 = eGGUIPage3;
			}
		}
		Vector2 vector = default(Vector2);
		vector = new Vector2(pageWidth, pageHeight);
		Vector2 vector2 = new Vector2(eGGUIPage.transform.localPosition.x, eGGUIPage.transform.localPosition.y) - vector / 2f;
		Vector2 vector3 = new Vector2(eGGUIPage2.transform.localPosition.x, eGGUIPage2.transform.localPosition.y) + vector / 2f;
		Vector2 vector4 = vector2 + (vector3 - vector2) / 2f;
		Vector2 vector5 = vector3 - vector2;
		return new Bounds(new Vector3(vector4.x, vector4.y), new Vector3(vector5.x, vector5.y));
	}

	private void Awake()
	{
		if (sourceObj == null)
		{
			if (GUIManager.instance != null && GUIManager.instance.GameFrame != null)
			{
				sourceObj = GUIManager.instance.GameFrame.gameObject;
			}
			else
			{
				sourceObj = GameObject.Find("GameFrame");
			}
		}
		if (sourceObj != null)
		{
			pageWidth = sourceObj.transform.localScale.x;
		}
		if (pageWidth <= 0f)
		{
			pageWidth = 640f;
		}
		pageNum = base.transform.childCount;
		mTrans = base.transform;
		mPanel = GetComponent<UIPanel>();
		UIPanel uIPanel = mPanel;
		uIPanel.onChange = (UIPanel.OnChangeDelegate)Delegate.Combine(uIPanel.onChange, new UIPanel.OnChangeDelegate(OnPanelChange));
		Pagging();
	}

	private void OnDestroy()
	{
		if (mPanel != null)
		{
			UIPanel uIPanel = mPanel;
			uIPanel.onChange = (UIPanel.OnChangeDelegate)Delegate.Remove(uIPanel.onChange, new UIPanel.OnChangeDelegate(OnPanelChange));
		}
	}

	private void OnPanelChange()
	{
		UpdateScrollbars(true);
	}

	private void Start()
	{
		UpdateScrollbars(true);
	}

	public bool RestrictWithinBounds(bool instant)
	{
		int page = -1;
		Vector2 min = bounds.min;
		Vector3 vector = CalculatePageConstrainOffset(min, bounds.max, out page);
		int selectPage = _selectPage;
		_selectPage = ((page >= 0) ? page : 0);
		if (selectPage != _selectPage)
		{
			mMomentum = Vector3.zero;
			if (onChangePage != null)
			{
				onChangePage(_selectPage);
			}
		}
		if (vector.magnitude > 0.001f)
		{
			if (!instant && dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				SpringPanel.Begin(mPanel.gameObject, mTrans.localPosition + vector, 13f);
			}
			else
			{
				MoveRelative(vector);
				mMomentum = Vector3.zero;
				mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	public void DisableSpring()
	{
		SpringPanel component = GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	public void UpdateScrollbars(bool recalculateBounds)
	{
		if (!(mPanel == null) & recalculateBounds)
		{
			mCalculatedBounds = false;
		}
	}

	public void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		DisableSpring();
		Bounds bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.y)
		{
			return;
		}
		Vector4 clipRange = mPanel.clipRange;
		float num = clipRange.z * 0.5f;
		float num2 = clipRange.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= mPanel.clipSoftness.x;
			num4 += mPanel.clipSoftness.x;
			num5 -= mPanel.clipSoftness.y;
			num6 += mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = mTrans.localPosition;
			if (scale.x != 0f)
			{
				localPosition.x += clipRange.x - num7;
			}
			if (scale.y != 0f)
			{
				localPosition.y += clipRange.y - num8;
			}
			mTrans.localPosition = localPosition;
		}
		clipRange.x = num7;
		clipRange.y = num8;
		mPanel.clipRange = clipRange;
		if (updateScrollbars)
		{
			UpdateScrollbars(false);
		}
	}

	public void ResetPosition()
	{
		mCalculatedBounds = false;
		SetDragAmount(relativePositionOnReset.x, relativePositionOnReset.y, false);
		SetDragAmount(relativePositionOnReset.x, relativePositionOnReset.y, true);
	}

	public void MoveRelative(Vector3 relative)
	{
		mTrans.localPosition += relative;
		Vector4 clipRange = mPanel.clipRange;
		clipRange.x -= relative.x;
		clipRange.y -= relative.y;
		mPanel.clipRange = clipRange;
		UpdateScrollbars(false);
	}

	public void MoveAbsolute(Vector3 absolute)
	{
		Vector3 vector = mTrans.InverseTransformPoint(absolute);
		Vector3 vector2 = mTrans.InverseTransformPoint(Vector3.zero);
		MoveRelative(vector - vector2);
	}

	public void Press(bool pressed)
	{
		if (smoothDragStart & pressed)
		{
			mDragStarted = false;
			mDragStartOffset = Vector2.zero;
		}
		if (!base.enabled || !NGUITools.GetActive(base.gameObject))
		{
			return;
		}
		if (!pressed && mDragID == UICamera.currentTouchID)
		{
			mDragID = -10;
		}
		mCalculatedBounds = false;
		mShouldMove = shouldMove;
		if (!mShouldMove)
		{
			return;
		}
		mPressed = pressed;
		if (pressed)
		{
			mMomentum = Vector3.zero;
			mScroll = 0f;
			DisableSpring();
			mLastPos = UICamera.lastHit.point;
			mPlane = new Plane(mTrans.rotation * Vector3.back, mLastPos);
			return;
		}
		if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None && dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
		{
			RestrictWithinBounds(false);
		}
		if (onDragFinished != null)
		{
			onDragFinished();
		}
	}

	public void Drag()
	{
		if (!base.enabled || !NGUITools.GetActive(base.gameObject) || !mShouldMove)
		{
			return;
		}
		if (mDragID == -10)
		{
			mDragID = UICamera.currentTouchID;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
			mDragStartOffset = UICamera.currentTouch.totalDelta;
		}
		Ray ray = ((!smoothDragStart) ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - mDragStartOffset));
		float enter = 0f;
		if (!mPlane.Raycast(ray, out enter))
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 vector = point - mLastPos;
		mLastPos = point;
		if (vector.x != 0f || vector.y != 0f)
		{
			vector = mTrans.InverseTransformDirection(vector);
			vector.Scale(scale);
			vector = mTrans.TransformDirection(vector);
		}
		mMomentum = Vector3.Lerp(mMomentum, mMomentum + vector * (0.01f * momentumAmount), 0.67f);
		if (!iOSDragEmulation)
		{
			MoveAbsolute(vector);
		}
		else
		{
			UIPanel uIPanel = mPanel;
			Vector2 min = bounds.min;
			if (uIPanel.CalculateConstrainOffset(min, bounds.max).magnitude > 0.001f)
			{
				MoveAbsolute(vector * 0.5f);
				mMomentum *= 0.5f;
			}
			else
			{
				MoveAbsolute(vector);
			}
		}
		if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None && dragEffect != UIDraggablePanel.DragEffect.MomentumAndSpring)
		{
			RestrictWithinBounds(true);
		}
	}

	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && scrollWheelFactor != 0f)
		{
			DisableSpring();
			mShouldMove = shouldMove;
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}

	private void LateUpdate()
	{
		if (repositionClipping)
		{
			repositionClipping = false;
			mCalculatedBounds = false;
			SetDragAmount(relativePositionOnReset.x, relativePositionOnReset.y, true);
		}
		if (!Application.isPlaying)
		{
			return;
		}
		float deltaTime = UpdateRealTimeDelta();
		if (mShouldMove && !mPressed)
		{
			mMomentum -= scale * (mScroll * 0.05f);
			if (mMomentum.magnitude > 0.0001f)
			{
				mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
				MoveAbsolute(absolute);
				if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None)
				{
					RestrictWithinBounds(false);
				}
				if (mMomentum.magnitude < 0.0001f && onDragFinished != null)
				{
					onDragFinished();
				}
				return;
			}
			mScroll = 0f;
			mMomentum = Vector3.zero;
		}
		else
		{
			mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
	}

	public static int SortByName(EGGUIPage a, EGGUIPage b)
	{
		return string.Compare(a.name, b.name);
	}

	public void Pagging()
	{
		UIPanel component = GetComponent<UIPanel>();
		if (component != null && (component.clipping == UIDrawCall.Clipping.AlphaClip || component.clipping == UIDrawCall.Clipping.SoftClip))
		{
			component.clipRange = new Vector4(0f, component.clipRange.y, pageWidth, component.clipRange.w);
			component.transform.localPosition = new Vector3(0f, component.transform.localPosition.y, component.transform.localPosition.z);
		}
		List<EGGUIPage> list = new List<EGGUIPage>(base.transform.GetComponentsInChildren<EGGUIPage>());
		list.Sort(SortByName);
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			Transform transform = list[i].transform;
			if (transform.gameObject.activeSelf)
			{
				transform.localPosition = new Vector3((float)num * pageWidth, 0f, 0f);
				num++;
			}
		}
		mBounds = CalculatePagesBounds(mTrans);
		MoveToPage(PageSelected);
	}

	private void Update()
	{
		if (sourceObj != null && sourceObj.transform.localScale.x != pageWidth)
		{
			pageWidth = sourceObj.transform.localScale.x;
			Pagging();
			RestrictWithinBounds(true);
		}
	}

	public Vector3 CalculatePageConstrainOffset(Vector2 min, Vector2 max, out int page)
	{
		if (mPanel == null)
		{
			page = 0;
			return Vector3.zero;
		}
		float num = mPanel.clipRange.z * 0.5f;
		float num2 = mPanel.clipRange.w * 0.5f;
		float x = mPanel.clipRange.x;
		float num3 = x - min.x;
		int num4 = (int)(num3 / pageWidth);
		if (num4 < 0)
		{
			num4 = 0;
		}
		if (num4 >= pageNum)
		{
			num4 = pageNum - 1;
		}
		page = num4;
		Vector2 vector = default(Vector2);
		vector = new Vector2(min.x + (float)num4 * pageWidth, min.y);
		Vector2 vector2 = default(Vector2);
		vector2 = new Vector2(vector.x + pageWidth, max.y);
		Vector2 vector3 = default(Vector2);
		vector3 = new Vector2(mPanel.clipRange.x - num, mPanel.clipRange.y - num2);
		Vector2 vector4 = default(Vector2);
		vector4 = new Vector2(mPanel.clipRange.x + num, mPanel.clipRange.y + num2);
		if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			vector3.x += mPanel.clipSoftness.x;
			vector3.y += mPanel.clipSoftness.y;
			vector4.x -= mPanel.clipSoftness.x;
			vector4.y -= mPanel.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(vector, vector2, vector3, vector4);
	}

	public void MoveToPage(int page)
	{
		if (page >= 0 && page < pageNum)
		{
			_selectPage = page;
			float num = mPanel.clipRange.z * 0.5f;
			float num2 = mPanel.clipRange.w * 0.5f;
			float x = this.bounds.min.x + (float)page * pageWidth;
			Bounds bounds = this.bounds;
			Vector2 vector = default(Vector2);
			vector = new Vector2(x, bounds.min.y);
			float x2 = vector.x + pageWidth;
			Bounds bounds2 = this.bounds;
			Vector2 vector2 = default(Vector2);
			vector2 = new Vector2(x2, bounds2.max.y);
			Vector2 vector3 = default(Vector2);
			vector3 = new Vector2(mPanel.clipRange.x - num, mPanel.clipRange.y - num2);
			Vector2 vector4 = default(Vector2);
			vector4 = new Vector2(mPanel.clipRange.x + num, mPanel.clipRange.y + num2);
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				vector3.x += mPanel.clipSoftness.x;
				vector3.y += mPanel.clipSoftness.y;
				vector4.x -= mPanel.clipSoftness.x;
				vector4.y -= mPanel.clipSoftness.y;
			}
			Vector3 relative = NGUIMath.ConstrainRect(vector, vector2, vector3, vector4);
			MoveRelative(relative);
			mMomentum = Vector3.zero;
			mScroll = 0f;
			if (onChangePage != null)
			{
				onChangePage(_selectPage);
			}
		}
	}
}
