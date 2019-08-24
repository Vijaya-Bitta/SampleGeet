using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SampleGeet
{
    class MusicLib
    {
        
            public string FileName { get; set; }
            public string Artist { get; set; }
            public string Album { get; set; }
            public TimeSpan Duration { get; set; }
            public string MusicPath { get; set; }
            public BitmapImage AlbumCover { get; set; }

        
    }
}
