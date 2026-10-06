using System;
using UnityEngine;

[AddComponentMenu("EG/GUI/EG Drag Object")]
public class EGGUIDragObject : IgnoreTimeScale
{
	public enum DragEffect
	{
		None = 0,
		Momentum = 1,
		MomentumAndSpring = 2
	}

	public Transform target;

	public UIPanel _panelDrag;

	public string _messageWhenPress = string.Empty;

	public string _messageWhenDrag = string.Empty;

	public Vector3 scale = Vector3.one;

	public float scrollWheelFactor;

	public bool restrictWithinPanel;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public float momentumAmount = 35f;

	public bool enableLastUpdate = true;

	private Plane mPlane;

	private Vector3 mLastPos;

	private bool mPressed;

	private Vector3 mMomentum = Vector3.zero;

	private float mScroll;

	private Bounds mBounds;

	public event Action<bool> onPressEvent;

	public event Action<bool, GameObject> onPressEventGO;

	public event Action<Vector2> onDragEvent;

	public event Action<Vector2, GameObject> onDragEventGO;

	private void FindPanel()
	{
		_panelDrag = ((!(target != null)) ? null : UIPanel.Find(target.transform, false));
		if (_panelDrag == null)
		{
			restrictWithinPanel = false;
		}
	}

	private void OnPress(bool pressed)
	{
		if (!base.enabled || !NGUITools.GetActive(base.gameObject) || !(target != null))
		{
			return;
		}
		mPressed = pressed;
		if (pressed)
		{
			if (restrictWithinPanel && _panelDrag == null)
			{
				FindPanel();
			}
			if (restrictWithinPanel)
			{
				mBounds = NGUIMath.CalculateRelativeWidgetBounds(_panelDrag.cachedTransform, target);
			}
			mMomentum = Vector3.zero;
			mScroll = 0f;
			SpringPosition component = target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			mLastPos = UICamera.lastHit.point;
			Transform transform = UICamera.currentCamera.transform;
			mPlane = new Plane(((!(_panelDrag != null)) ? transform.rotation : _panelDrag.cachedTransform.rotation) * Vector3.back, mLastPos);
		}
		else if (restrictWithinPanel && _panelDrag.clipping != UIDrawCall.Clipping.None && dragEffect == DragEffect.MomentumAndSpring)
		{
			_panelDrag.ConstrainTargetToBounds(target, ref mBounds, false);
		}
		if (onPressEvent != null)
		{
			onPressEvent(mPressed);
		}
		if (onPressEventGO != null)
		{
			onPressEventGO(mPressed, target.gameObject);
		}
		if (_messageWhenPress.Length > 0)
		{
			target.SendMessage(_messageWhenPress, mPressed);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		if (!base.enabled || !NGUITools.GetActive(base.gameObject) || !(target != null))
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
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
			vector = target.InverseTransformDirection(vector);
			vector.Scale(scale);
			vector = target.TransformDirection(vector);
		}
		if (dragEffect != DragEffect.None)
		{
			mMomentum = Vector3.Lerp(mMomentum, mMomentum + vector * (0.01f * momentumAmount), 0.67f);
		}
		if (restrictWithinPanel)
		{
			Vector3 localPosition = target.localPosition;
			target.position += vector;
			mBounds.center += target.localPosition - localPosition;
			if (dragEffect != DragEffect.MomentumAndSpring && _panelDrag.clipping != UIDrawCall.Clipping.None && _panelDrag.ConstrainTargetToBounds(target, ref mBounds, true))
			{
				mMomentum = Vector3.zero;
				mScroll = 0f;
			}
		}
		else
		{
			target.position += vector;
		}
		if (_messageWhenDrag.Length > 0)
		{
			target.SendMessage(_messageWhenDrag);
		}
		if (onDragEvent != null)
		{
			onDragEvent(delta);
		}
		if (onDragEventGO != null)
		{
			onDragEventGO(delta, target.gameObject);
		}
	}

	private void LateUpdate()
	{
		if (!enableLastUpdate)
		{
			return;
		}
		float deltaTime = UpdateRealTimeDelta();
		if (target == null)
		{
			return;
		}
		if (mPressed)
		{
			SpringPosition component = target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			mScroll = 0f;
		}
		else
		{
			mMomentum += scale * ((0f - mScroll) * 0.05f);
			mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
			if (mMomentum.magnitude > 0.0001f)
			{
				if (_panelDrag == null)
				{
					FindPanel();
				}
				if (_panelDrag != null)
				{
					target.position += NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
					if (!restrictWithinPanel || _panelDrag.clipping == UIDrawCall.Clipping.None)
					{
						return;
					}
					mBounds = NGUIMath.CalculateRelativeWidgetBounds(_panelDrag.cachedTransform, target);
					if (!_panelDrag.ConstrainTargetToBounds(target, ref mBounds, dragEffect == DragEffect.None))
					{
						SpringPosition component2 = target.GetComponent<SpringPosition>();
						if (component2 != null)
						{
							component2.enabled = false;
						}
					}
					return;
				}
			}
			else
			{
				mScroll = 0f;
			}
		}
		NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
	}

	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}
}
