using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AlternativeGamesTest.Scenario
{
    public abstract class ScenarioBase : MonoBehaviour
    {
        protected ScenarioData Data;
        private CancellationTokenSource _source;

        public CancellationToken Token
        {
            get
            {
                if (_source != null)
                {
                    return _source.Token;
                }

                return default;
            }
        }
    
        public void Init(ScenarioData scenarioData)
        {
            Data = scenarioData;
            OnInit();
        }

        protected virtual void OnInit()
        {
        }

        public async Task Run(CancellationToken token)
        {
            _source = CancellationTokenSource.CreateLinkedTokenSource(token);
            await RunInternal(Token);
            Stop();
        }


        public void Stop()
        {
            OnStop();
        }

        public void UpdateScenario(float deltaTime)
        {
            OnUpdate(deltaTime);
        }

        protected abstract Task RunInternal(CancellationToken token);

        protected abstract void OnStop();

        protected virtual void OnUpdate(float deltaTime)
        {
        
        }
    }
}