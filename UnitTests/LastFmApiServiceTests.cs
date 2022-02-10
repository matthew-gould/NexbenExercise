using Microsoft.VisualStudio.TestTools.UnitTesting;
using NexbenExercise.Models;
using NexbenExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class LastFmApiServiceTests
    {
        TayTayBae mockTayTayBae;
        LastFmModel mockFmModel;
        ILastFmApiService _service;

        [TestInitialize]
        public void Setup()
        {
            mockTayTayBae = new TayTayBae()
            {
                TopTracks = new TopTracks()
                {
                    Track = new List<Track>()
                    {
                        new Track()
                        {
                            Name = "True TayTay Love",
                            RankMeta = new Meta()
                            {
                                Rank = 1
                            }
                        },
                        new Track()
                        {
                            Name = "Love Tests Like TayTay",
                            RankMeta = new Meta()
                            {
                                Rank = 2
                            }
                        },
                    }
                }
            };

            mockFmModel = new LastFmModel()
            {
                Tracks = new Tracks()
                {
                    Track = new List<Track>()
                    {
                        new Track()
                        {
                            Artist = new Artist()
                            {
                                Name = "TayTay"
                            },
                            Playcount = 5
                        },
                        new Track()
                        {
                            Artist = new Artist()
                            {
                                Name = "TayTay"
                            },
                            Playcount = 4
                        },
                        new Track()
                        {
                            Artist = new Artist()
                            {
                                Name = "NotTayTay"
                            },
                            Playcount = 3
                        }
                    }
                }
            };

            _service = new LastFmApiService();
        }

        [TestMethod]
        public void GetTopRankedSong_Should_Return_Rank_1_Song()
        {
            var result = _service.GetTopRankedSong(mockTayTayBae);

            Assert.AreEqual(result, "True TayTay Love");
        }

        [TestMethod]
        public void SortArtists_Should_Return_Sorted_List()
        {
            var result = _service.SortArtists(mockFmModel).ToList();

            Assert.AreEqual(result[0].Key, "TayTay");
            Assert.AreEqual(result[0].Value.SongCount, 2);
            Assert.AreEqual(result[0].Value.TotalPlaycount, 9);
            Assert.AreEqual(result[0].Value.DivisibleByNine, true);
            Assert.AreEqual(result[1].Key, "NotTayTay");
            Assert.AreEqual(result[1].Value.SongCount, 1);
            Assert.AreEqual(result[1].Value.TotalPlaycount, 3);
            Assert.AreEqual(result[1].Value.DivisibleByNine, false);
        }

        [TestMethod]
        public void Testing_Division_Math()
        {
            var result = Math.Ceiling(25 / 2.0);

            Assert.AreEqual(result, 13);
        }

        [TestMethod]
        public void Testing_Division_Math2()
        {
            var result = Math.Ceiling(12 / 2.0);

            Assert.AreEqual(result, 6);
        }
    }
}
