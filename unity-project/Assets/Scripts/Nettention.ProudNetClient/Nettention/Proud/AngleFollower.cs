using System;

namespace Nettention.Proud
{
	public class AngleFollower
	{
		private double followerAngle;

		private double targetAngle;

		private double followerAngleVelocity;

		public double FollowerAngle
		{
			get
			{
				return followerAngle;
			}
			set
			{
				followerAngle = value;
			}
		}

		public double TargetAngle
		{
			get
			{
				return targetAngle;
			}
			set
			{
				targetAngle = value % (Sysutil.PROUDNET_PI * 2.0);
			}
		}

		public double FollowerAngleVelocity
		{
			get
			{
				return followerAngleVelocity;
			}
			set
			{
				followerAngleVelocity = Math.Max(Sysutil.PROUDNET_PI / 180.0 * 0.0010000000474974513, value);
			}
		}

		public void FrameMove(double elapsedTime)
		{
			followerAngle %= 2.0 * Sysutil.PROUDNET_PI;
			if (targetAngle + Sysutil.PROUDNET_PI < followerAngle)
			{
				followerAngle -= 2.0 * Sysutil.PROUDNET_PI;
			}
			if (targetAngle - Sysutil.PROUDNET_PI > followerAngle)
			{
				followerAngle += 2.0 * Sysutil.PROUDNET_PI;
			}
			double num = elapsedTime * followerAngleVelocity;
			double num2 = targetAngle - followerAngle;
			num2 = ((!(num2 < 0.0)) ? Math.Min(num, num2) : Math.Max(0.0 - num, num2));
			followerAngle += num2;
		}
	}
}
