using System;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour {
    private const string ChannelId = "simple_driving_energy_channel";
    
    public void ScheduleNotification(DateTime dateTime) {
#if UNITY_ANDROID
        var notificationChannel = new AndroidNotificationChannel {
            Id = ChannelId,
            Name = "Simple Driving Energy",
            Description = "The notification channel for the energy replenishment of the Simple Driving game",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        var notification = new AndroidNotification {
            Title = "Energy Recharged!",
            Text = "Your energy has recharged, come back to keep driving!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
#endif
    }
}
