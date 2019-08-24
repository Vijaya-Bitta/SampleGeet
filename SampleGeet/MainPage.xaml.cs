using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Media.Playback;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Streams;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleGeet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<MusicLib> MusicList = new ObservableCollection<MusicLib>();
        MediaElement Player = new MediaElement();

        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFolder musicLib = KnownFolders.MusicLibrary;
            var files = await musicLib.GetFilesAsync();

            foreach (var file in files)
            {
                StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 200, ThumbnailOptions.UseCurrentScale);
                var albumCover = new BitmapImage();
                albumCover.SetSource(currentThumb);

                var musicProperties = await file.Properties.GetMusicPropertiesAsync();
                var musicname = musicProperties.Title;
                var musicdur = musicProperties.Duration;

                var artist = musicProperties.Artist;
                if (artist == "")
                {
                    artist = "Artista desconocido";
                }

                var album = musicProperties.Album;
                if (album == "")
                {
                    album = "Unkown";
                }
                MusicList.Add(new MusicLib
                {
                    FileName = musicname,
                    Artist = artist,
                    Album = album,
                    Duration = musicdur,
                    AlbumCover = albumCover,
                    MusicPath = file.Path
                });

            }
            
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mylist.SelectionChanged += GridView_SelectionChanged;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    MusicLib clicked = (MusicLib)e.ClickedItem;
        //    // Playing the file
        //    string path = clicked.MusicPath;

        //    //code to play song from song.MusicPath
        //    MediaElement player = new MediaElement();
        //    var uri = new Uri(path);

        //    player.Source = uri;

        //    player.Play();

        //}
        private async void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView view = (GridView)sender;
            //Get Selected Item
            MusicLib song = view.SelectedItem as MusicLib;
            string path = song.MusicPath;

            //code to play song from song.MusicPath
            var file = await StorageFile.GetFileFromPathAsync(path);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                Player.SetSource(stream, file.ContentType);

                if (Player.DefaultPlaybackRate != 1)
                {
                    Player.DefaultPlaybackRate = 1.0;
                }

                Player.Play();
            }
            
            //can then control the playback of the song through the player object then
        }
    }
}
