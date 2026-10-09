using System;
using System.IO;
using System.Media;

namespace MarioGameSystem.Helpers
{
    public class SoundManager
    {
        private SoundPlayer bgmPlayer;
        private SoundPlayer sfxPlayer;
        private bool isMuted;

        public bool IsMuted
        {
            get => isMuted;
            set
            {
                isMuted = value;
                if (isMuted) StopBGM();
                else PlayBGM();
            }
        }

        public SoundManager(string bgmPath, string sfxPath)
        {
            try
            {
                if (File.Exists(bgmPath)) bgmPlayer = new SoundPlayer(bgmPath);
                if (File.Exists(sfxPath)) sfxPlayer = new SoundPlayer(sfxPath);
            }
            catch { }
        }

        public void PlayBGM()
        {
            try { if (!isMuted && bgmPlayer != null) bgmPlayer.PlayLooping(); } catch { }
        }

        public void StopBGM()
        {
            try { bgmPlayer?.Stop(); } catch { }
        }

        public void PlaySFX()
        {
            try { if (!isMuted && sfxPlayer != null) sfxPlayer.Play(); } catch { }
        }
    }
}