using System;
using BubbleJump.Infrastructure.Time;
using UnityEngine;

namespace BubbleJump.Model.SuperJump
{
    public class SuperJumpService : ISuperJumpService, IDisposable
    {

        private const long TimeToChargeTicks = TimeSpan.TicksPerSecond * 5;

        private const float ChargeMaxPercentage = 0.2f;
        private const float ChargeMinPercentage = 0.001f;

        private const float ChargeMinThreshold = 0.8f;

        private const int PerfectClicksPerSecond = 20;

        private const float MaxStr = 1000f;

        private const float StrLoss = ChargeMinPercentage * PerfectClicksPerSecond;
        
        private readonly ITimeManager _timeManager;
        private readonly ISuperJumpMutableModel _superJumpModel;

        public event Action<float> Jumped;

        public SuperJumpService(
            ITimeManager timeManager,
            ISuperJumpMutableModel superJumpModel)
        {
            _timeManager = timeManager;
            _superJumpModel = superJumpModel;
            
            _superJumpModel.SetMaxStrength(MaxStr);
            _timeManager.CurrentTimestampUtc.Bind(OnTick);
        }

        private void OnTick(long t1, long t2)
        {
            var dt = (t2 - t1) / (float)TimeSpan.TicksPerSecond;
            _superJumpModel.SetStrength(Mathf.Max(0f, _superJumpModel.Strength.Value - StrLoss * dt));
        }

        public void Charge()
        {
            if (!_superJumpModel.JumpPlannedTime.Value.HasValue)
            {
                var plannedTime = _timeManager.CurrentTimestampUtc.Value + TimeToChargeTicks;
                _timeManager.AddCallback(plannedTime, Jump);
                _superJumpModel.SetPlannedTime(plannedTime);
            }

            var currentStr = _superJumpModel.Strength.Value;
            var maxStr = _superJumpModel.MaxStrength.Value;
            var strPercent = currentStr / maxStr;
            var strFactor = Mathf.Clamp(strPercent / ChargeMinThreshold, 0f, 1f);
            var chargeStrPercent = Mathf.Lerp(ChargeMaxPercentage, ChargeMinPercentage, strFactor);
            var chargeStr = maxStr * chargeStrPercent;
            _superJumpModel.SetStrength(Mathf.Clamp(currentStr + chargeStr, 0f , maxStr));
        }

        private void Jump()
        {
            Jumped?.Invoke(_superJumpModel.Strength.Value);
            _superJumpModel.SetPlannedTime(null);
            _superJumpModel.SetStrength(0f);
        }


        public void Dispose()
        {
            _timeManager.CurrentTimestampUtc.Unbind(OnTick);
        }
    }
}