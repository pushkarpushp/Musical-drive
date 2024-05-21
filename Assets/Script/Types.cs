
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class Types : MonoBehaviour
{
    public class Handle
    {
        [JsonProperty("fullHandle")]
        public string FullHandle { get; set; }
    }

    public class Raw
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Picture
    {
        [JsonProperty("raw")]
        public Raw Raw { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }

    public class By
    {
        [JsonProperty("handle")]
        public Handle Handle { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class Optimized
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Audio
    {
        [JsonProperty("optimized")]
        public Optimized Optimized { get; set; }
    }

    public class Cover
    {
        [JsonProperty("optimized")]
        public Optimized Optimized { get; set; }
    }

    public class Asset
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("audio")]
        public Audio Audio { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }
    }

    public class Metadata2
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("rawURI")]
        public string RawURI { get; set; }

        [JsonProperty("asset")]
        public Asset Asset { get; set; }
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("by")]
        public By By { get; set; }

        [JsonProperty("metadata")]
        public Metadata2 Metadata { get; set; }
    }

    public class PageInfo
    {
        [JsonProperty("next")]
        public string Next { get; set; }
    }

    public class ExplorePublications
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }
    }

    public class ExplorePublicationsData
    {
        [JsonProperty("explorePublications")]
        public ExplorePublications ExplorePublications { get; set; }
    }

    public class ExplorePublicationsRoot
    {
        [JsonProperty("data")]
        public ExplorePublicationsData Data { get; set; }
    }

}