using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EcisApi.Helpers
{
    public static class Extensions
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(async delegate
                {
                    using (partition)
                        while (partition.MoveNext())
                            await body(partition.Current);
                }));
        }

        public static async Task AsyncParallelForEach<T>(this IAsyncEnumerable<T> source, Func<T, Task> body, int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            if (scheduler != null)
                options.TaskScheduler = scheduler;

            var block = new ActionBlock<T>(body, options);

            await foreach (var item in source)
                block.Post(item);

            block.Complete();
            await block.Completion;
        }

        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                yield return await Task.FromResult(item);
            }
        }

        public static DateTime AddByType(this DateTime dateTime, int amount, string type)
        {
            var result = type.ToLower() switch
            {
                "second" or "seconds" => dateTime.AddSeconds(amount),
                "minute" or "minutes" => dateTime.AddMinutes(amount),
                "hour" or "hours" => dateTime.AddHours(amount),
                "day" or "days" => dateTime.AddDays(amount),
                "month" or "months" => dateTime.AddMonths(amount),
                "year" or "years" => dateTime.AddYears(amount),
                _ => dateTime
            };
            return result;
        }

    }
}
