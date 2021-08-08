# MobileKit

### Xamarin (iOS & Android) apps

## Features

**BackgroundTask**

Create long lived tasks that will continue to run after your app is placed into the background.  This also allow you to run a timer in the background.  

**Example: Sleep Timer**

```c#
        var player = Audio.Instance();

        player.Play("crickets.mp3", true);

        BackgroundTask.Instance.Run(TimeSpan.FromMinutes(1), () =>
        {
            player.Stop();
            player.Play("alert.mp3");
        });
```



**Audio**

A simple wrapper to play audio files.

```c#
Audio.Instance().Play("alert.mp3");
```

**Brightness**

Control the screen brightness

**Example: Set screen brightness on a 0.0 - 1.0 scale**

```c#
Brightness.Set(0.7f)
```

