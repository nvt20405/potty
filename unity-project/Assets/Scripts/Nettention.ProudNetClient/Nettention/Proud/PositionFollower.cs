using System;

namespace Nettention.Proud
{
	public class PositionFollower
	{
		private Vector3 targetPos;

		private Vector3 targetVel;

		private Vector3 followerPos;

		private Vector3 followerVel;

		private Vector3 gravity;

		private double followDuration = 0.2;

		private bool used;

		private double targetPosOrVelSetElapsedTime;

		private bool autoFollowDuration = true;

		private Vector3[] splineCoord = new Vector3[4];

		private double splineTimeslice;

		private double splineTerm;

		private Vector3 splineFollowerVel;

		private Vector3 splineFollowerPos;

		private double warpThresholdSqu;

		private static readonly double veryLowAngleCos = Math.Cos(Sysutil.PROUDNET_PI / 180.0 * 1.0);

		private double SplineCoord23Term
		{
			get
			{
				return Math.Max(0.01, followDuration * 0.66);
			}
		}

		private double SplineCoord01Term
		{
			get
			{
				return Math.Max(0.01, followDuration * 0.66);
			}
		}

		private double SplineMaxTimeslice
		{
			get
			{
				return followDuration * 1.1;
			}
		}

		private Vector3 AdjustedSplineFollowerVel
		{
			get
			{
				double length = targetVel.Length;
				Vector3 result = splineFollowerVel;
				Vector3 result2 = result.Normal * length;
				if (result.Length > length)
				{
					return result2;
				}
				return result;
			}
		}

		private double SplineCoord23FutureTerm
		{
			get
			{
				return Math.Max(0.1, followDuration * 2.0);
			}
		}

		public double FollowDuration
		{
			get
			{
				return followDuration;
			}
			set
			{
				if (!autoFollowDuration)
				{
					followDuration = value;
				}
			}
		}

		public bool IsFirstUse
		{
			get
			{
				return !used;
			}
		}

		public double WarpThreshold
		{
			set
			{
				warpThresholdSqu = Math.Pow(value, 2.0);
			}
		}

		public Vector3 FollowerPosition
		{
			get
			{
				return followerPos;
			}
			set
			{
				SetFollower(value, FollowerVelocity);
			}
		}

		public Vector3 FollowerVelocity
		{
			get
			{
				return followerVel;
			}
			set
			{
				SetFollower(FollowerPosition, value);
			}
		}

		public Vector3 TargetPosition
		{
			get
			{
				return targetPos;
			}
			set
			{
				SetTarget(value, TargetVelocity);
			}
		}

		public Vector3 TargetVelocity
		{
			get
			{
				return targetVel;
			}
			set
			{
				SetTarget(TargetPosition, value);
			}
		}

		public Vector3 Gravity
		{
			get
			{
				return gravity;
			}
			set
			{
				gravity = value;
			}
		}

		public bool EnableAutoFollowDuration
		{
			set
			{
				autoFollowDuration = value;
			}
		}

		public Vector3 SplineFollowerPosition
		{
			get
			{
				return splineFollowerPos;
			}
		}

		public Vector3 SplineFollowerVelocity
		{
			get
			{
				return splineFollowerVel;
			}
		}

		public void SetFollower(Vector3 position, Vector3 velocity)
		{
			bool flag = false;
			bool flag2 = false;
			if (followerPos != position)
			{
				followerPos = position;
				flag = true;
			}
			if (followerVel != velocity)
			{
				followerVel = velocity;
				flag2 = true;
			}
			if (flag || flag2)
			{
				splineCoord[0] = position;
				splineCoord[1] = position + velocity * SplineCoord01Term;
				splineTimeslice = 0.0;
				splineTerm = SplineMaxTimeslice;
			}
		}

		public void SetTarget(Vector3 position, Vector3 velocity)
		{
			bool flag = false;
			bool flag2 = false;
			if (targetPos != position)
			{
				flag = true;
			}
			if (targetVel != velocity)
			{
				flag2 = true;
			}
			if (!used)
			{
				followerPos = position;
				followerVel = velocity;
			}
			if (autoFollowDuration && targetPosOrVelSetElapsedTime > 0.0)
			{
				double num = targetPosOrVelSetElapsedTime;
				followDuration = Sysutil.Lerp(followDuration, num * 1.3, 0.8);
			}
			targetPosOrVelSetElapsedTime = 0.0;
			if (flag || flag2)
			{
				Vector3 vector = targetPos + targetVel * followDuration;
				Vector3 vector2 = position + velocity * followDuration;
				Vector3 vector3 = vector2 - vector;
				double length = targetVel.Length;
				if (length > 0.0 && Vector3.Dot(-vector3.Normal, velocity.Normal) > veryLowAngleCos && vector3.Length / length < 0.1)
				{
					position -= vector3;
				}
			}
			targetPos = position;
			targetVel = velocity;
			if (flag || flag2)
			{
				splineCoord[0] = splineFollowerPos;
				splineCoord[1] = splineFollowerPos + AdjustedSplineFollowerVel * SplineCoord01Term;
				splineCoord[2] = position + velocity * followDuration;
				splineCoord[3] = splineCoord[2] - velocity * SplineCoord23Term;
				splineTimeslice = 0.0;
				splineTerm = SplineMaxTimeslice;
			}
		}

		public void FrameMove(double elapsedTime)
		{
			double lengthSq = (targetPos - followerPos).LengthSq;
			if (warpThresholdSqu > 0.0 && lengthSq > warpThresholdSqu)
			{
				EqualizeFollowerToTarget();
			}
			else
			{
				Vector3 vector = targetPos;
				Vector3 vector2 = followerPos;
				targetVel += gravity * elapsedTime;
				targetPos += targetVel * elapsedTime;
				targetPosOrVelSetElapsedTime += elapsedTime;
				double val = followDuration - targetPosOrVelSetElapsedTime;
				val = Math.Max(val, elapsedTime);
				Vector3 vector3 = targetPos + targetVel * val;
				followerVel = (vector3 - followerPos) / val;
				double length = targetVel.Length;
				if (followerVel.Length < length)
				{
					followerVel.Length = length;
				}
				followerVel += gravity * elapsedTime;
				followerPos += followerVel * elapsedTime;
				double length2 = (vector - vector2).Length;
				double length3 = (targetPos - followerPos).Length;
				if (length3 > length2)
				{
					EqualizeFollowerToTarget();
				}
			}
			used = true;
			if (splineTimeslice >= splineTerm)
			{
				splineFollowerPos = targetPos;
				splineFollowerVel = targetVel;
				return;
			}
			double ratio = splineTimeslice / splineTerm;
			Vector3 a = splineCoord[0];
			Vector3 vector4 = splineCoord[1];
			Vector3 vector5 = splineCoord[2];
			Vector3 b = splineCoord[3];
			Vector3 a2 = Vector3.Lerp(a, vector4, ratio);
			Vector3 vector6 = Vector3.Lerp(vector4, vector5, ratio);
			Vector3 b2 = Vector3.Lerp(vector5, b, ratio);
			Vector3 a3 = Vector3.Lerp(a2, vector6, ratio);
			Vector3 b3 = Vector3.Lerp(vector6, b2, ratio);
			Vector3 vector7 = Vector3.Lerp(a3, b3, ratio);
			Vector3 vector8 = splineFollowerPos;
			splineFollowerPos = vector7;
			if (elapsedTime > 0.0)
			{
				splineFollowerVel = (splineFollowerPos - vector8) / elapsedTime;
			}
			splineTimeslice += elapsedTime;
		}

		private void EqualizeFollowerToTarget()
		{
			followerPos = targetPos;
			followerVel = targetVel;
			splineCoord[0] = targetPos;
			splineCoord[1] = targetPos + targetVel * SplineCoord01Term;
			splineTerm = (splineTimeslice = SplineMaxTimeslice);
		}

		public void GetFollower(ref Vector3 position, ref Vector3 velocity)
		{
			position = followerPos;
			velocity = followerVel;
		}

		public void GetTarget(ref Vector3 position, ref Vector3 velocity)
		{
			position = targetPos;
			velocity = targetVel;
		}
	}
}
