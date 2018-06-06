﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISTING_4_38_LINQ_aggregate
{
    class Artist
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    class MusicTrack
    {
        public int ID { get; set; }
        public int ArtistID { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] artistNames = new string[] { "Rob Miles", "Fred Bloggs", "The Bloggs Singers", "Immy Brown" };
            string[] titleNames = new string[] { "My Way", "Your Way", "His Way", "Her Way", "Milky Way" };

            List<Artist> artists = new List<Artist>();
            List<MusicTrack> musicTracks = new List<MusicTrack>();

            Random rand = new Random(1);
            int IDcount = 0;

            foreach (string artistName in artistNames)
            {
                Artist newArtist = new Artist { ID = IDcount++, Name = artistName };
                artists.Add(newArtist);
                foreach (string titleName in titleNames)
                {
                    MusicTrack newTrack = new MusicTrack
                    {
                        ID = IDcount++,
                        ArtistID = newArtist.ID,
                        Title = titleName,
                        Length = rand.Next(20, 600)
                    };
                    musicTracks.Add(newTrack);
                }
            }

            var artistSummary = from artist in artists
                                join track in musicTracks on artist.ID equals track.ArtistID
                                group track by artist.Name into artistTrackSummary
                                select  
                                new
                                {
                                    ArtistName = artistTrackSummary.Key,
                                    Tracks = artistTrackSummary.Count(),
                                    Length = artistTrackSummary.Sum(x=>x.Length)
                                };

            foreach ( var summary in artistSummary)
            {
                Console.WriteLine("Name:{0} Tracks:{1} Length:{2}",
                    summary.ArtistName, summary.Tracks, summary.Length);
            }
            Console.ReadKey();
        }
    }
}
