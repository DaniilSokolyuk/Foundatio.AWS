﻿using System;
using System.Threading.Tasks;
using Amazon.Runtime;
using Foundatio.Logging;
using Foundatio.Queues;
using Foundatio.Tests.Queue;
using Foundatio.Tests.Utility;
using Xunit;
using Xunit.Abstractions;

namespace Foundatio.AWS.Tests.Queues {
    public class SQSQueueTests : QueueTestBase {
        private readonly string _queueName = "foundatio-" + Guid.NewGuid().ToString("N").Substring(10);

        public SQSQueueTests(ITestOutputHelper output) : base(output) {}

        protected override IQueue<SimpleWorkItem> GetQueue(int retries = 1, TimeSpan? workItemTimeout = null, TimeSpan? retryDelay = null, int deadLetterMaxItems = 100, bool runQueueMaintenance = true) {
            // Don't run this as part of the tests yet
            return null;

            var section = Configuration.GetSection("AWS");
            string accessKey = section["ACCESS_KEY_ID"];
            string secretKey = section["SECRET_ACCESS_KEY"];
            if (String.IsNullOrEmpty(accessKey) || String.IsNullOrEmpty(secretKey))
                return null;

            var queue = new SQSQueue<SimpleWorkItem>(new SQSQueueOptions<SimpleWorkItem> {
                Credentials = new BasicAWSCredentials(accessKey, secretKey),
                Name = _queueName,
                Retries = retries,
                WorkItemTimeout = workItemTimeout.GetValueOrDefault(TimeSpan.FromMinutes(5)),
                LoggerFactory = Log,
            });

            _logger.Debug("Queue Id: {queueId}", queue.QueueId);
            return queue;
        }

        [Fact]
        public override async Task CanQueueAndDequeueWorkItemAsync() {
            await base.CanQueueAndDequeueWorkItemAsync().ConfigureAwait(false);
        }

        [Fact]
        public override async Task CanDequeueWithCancelledTokenAsync() {
            await base.CanDequeueWithCancelledTokenAsync();
        }

        [Fact]
        public override async Task CanQueueAndDequeueMultipleWorkItemsAsync() {
            await base.CanQueueAndDequeueMultipleWorkItemsAsync();
        }

        [Fact]
        public override async Task WillWaitForItemAsync() {
            await base.WillWaitForItemAsync();
        }

        [Fact]
        public override async Task DequeueWaitWillGetSignaledAsync() {
            await base.DequeueWaitWillGetSignaledAsync();
        }

        [Fact]
        public override async Task CanUseQueueWorkerAsync() {
            await base.CanUseQueueWorkerAsync();
        }

        [Fact]
        public override async Task CanHandleErrorInWorkerAsync() {
            await base.CanHandleErrorInWorkerAsync();
        }

        [Fact]
        public override async Task WorkItemsWillTimeoutAsync() {
            await base.WorkItemsWillTimeoutAsync();
        }

        [Fact]
        public override async Task WorkItemsWillGetMovedToDeadletterAsync() {
            await base.WorkItemsWillGetMovedToDeadletterAsync();
        }

        [Fact]
        public override async Task CanAutoCompleteWorkerAsync() {
            await base.CanAutoCompleteWorkerAsync();
        }

        [Fact]
        public override async Task CanHaveMultipleQueueInstancesAsync() {
            await base.CanHaveMultipleQueueInstancesAsync();
        }

        [Fact]
        public override async Task CanRunWorkItemWithMetricsAsync() {
            await base.CanRunWorkItemWithMetricsAsync();
        }

        [Fact]
        public override async Task CanRenewLockAsync() {
            await base.CanRenewLockAsync();
        }

        [Fact]
        public override async Task CanAbandonQueueEntryOnceAsync() {
            await base.CanAbandonQueueEntryOnceAsync();
        }

        [Fact]
        public override async Task CanCompleteQueueEntryOnceAsync() {
            await base.CanCompleteQueueEntryOnceAsync();
        }

        // NOTE: Not using this test because you can set specific delay times for storage queue
        public override async Task CanDelayRetryAsync() {
            await base.CanDelayRetryAsync();
        }

        public override void Dispose() {
            // do nothing
        }
    }
}
