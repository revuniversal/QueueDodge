using System;
using System.Diagnostics;
using System.Threading;

using QueueDodge.Models;

namespace QueueDodge.Services
{
    public class RequestService
    {
        private const int Second = 1000;

        private QueueDodgeDB data;

        public RequestService(QueueDodgeDB data) 
        {
            this.data = data;
        }

        /// <summary>
        /// Performs and records the specified routine and upon failure, retries with an exponentional backoff.
        /// </summary>
        /// <param name="retries">The number of times to retry.</param>
        /// <param name="intervalMin">The minimum interval to back off.</param>
        /// <param name="intervalMax">The maximum interval to back off.</param>
        /// <param name="request">The request to record for this operation.</param>
        /// <param name="routine">The procedure to execute.</param>
        public void Perform(int retries, int intervalMin, int intervalMax, BattleNetRequest request, Action routine)
        {
            int retryCount = retries;
            int maxInterval = intervalMax;
            int backOffInterval = intervalMin;
            int exponent = 2;
            int currentCount = 0;

            do
            {
                try
                {
                    var timer = new Stopwatch();
                    timer.Start();

                    routine();

                    timer.Stop();
                    data.BattleNetRequests.Attach(request);
                    request.Duration = timer.Elapsed.TotalSeconds;
                    data.SaveChanges();
                    break;
                }
                catch (Exception ex)
                {
                    var x = ex;
                    if (++currentCount >= retryCount) break;

                    Thread.Sleep(backOffInterval * Second);
                    backOffInterval = Math.Min(maxInterval, backOffInterval * exponent);
                }
            } while (true);


        }
    }
}
