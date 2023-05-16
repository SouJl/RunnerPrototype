using System;
using UnityEngine;

namespace Runner.Tool.Notification.Push.Settings
{
    [Serializable]
    internal class NotificationData
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public NotificationRepeat RepeatType { get; private set; }
        [field: SerializeField] public Date FireTime { get; private set; }
        [field: SerializeField] public Span RepeatInterval { get; private set; }

        public override string ToString() => $"{Id}: {Title}.{Text}. {RepeatType:F}, {FireTime}, {RepeatInterval}";
    }

    internal enum NotificationRepeat
    {
        Once,
        Repeatable
    }

    [Serializable]
    internal class Date
    {
        [field: SerializeField, Min(1)] public int Year { get; private set; }
        [field: SerializeField, Range(1, 12)] public int Month { get; private set; }
        [field: SerializeField, Range(1, 31)] public int Day { get; private set; }
        [field: SerializeField, Range(0, 23)] public int Hour { get; private set; }
        [field: SerializeField, Range(0, 59)] public int Minute { get; private set; }

        public static implicit operator DateTime(Date date) 
            => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, default);
    }

    [Serializable]
    internal class Span
    {
        [field: SerializeField, Min(0)] public int Seconds { get; private set; }

        public override string ToString() => Seconds.ToString();

        public static implicit operator TimeSpan(Span span) => TimeSpan.FromSeconds(span.Seconds);
    }
}
