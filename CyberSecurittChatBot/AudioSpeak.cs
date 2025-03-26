
using NAudio.Wave;

public class AudioPlayer
{
    private string _audioFilePath;

    public AudioPlayer(string audioFilePath)
    {
        _audioFilePath = audioFilePath;
    }

    public void Play()
    {
        using (var audioFile = new AudioFileReader(_audioFilePath))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();
            Console.WriteLine("Playing audio...");

            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                System.Threading.Thread.Sleep(1000); // Keep the application running while audio plays
            }
        }
    }
}