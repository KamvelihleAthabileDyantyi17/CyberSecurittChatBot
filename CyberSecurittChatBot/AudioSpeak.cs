using System;
using NAudio.Wave;

public class AudioPlayer
{
    private string _audioFilePath;

    // Constructor to initialize the audio file path
    public AudioPlayer(string audioFilePath)
    {
        _audioFilePath = audioFilePath;
    }

    // Method to play the audio file
    public void Play()
    {
        using (var audioFile = new AudioFileReader(_audioFilePath))
        using (var outputDevice = new WaveOutEvent())
        {
            // Initialize the output device with the audio file
            outputDevice.Init(audioFile);
            outputDevice.Play();
            Console.WriteLine("Playing audio...");

            // Keep the application running while the audio plays
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
