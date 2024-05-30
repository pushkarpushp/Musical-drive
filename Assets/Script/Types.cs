
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

    public class Optimized
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Picture
    {
        [JsonProperty("optimized")]
        public Optimized Optimized { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }

    public class Profile
    {
        [JsonProperty("handle")]
        public Handle Handle { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Operations
    {
        [JsonProperty("hasActed")]
        public HasActed HasActed { get; set; }

        [JsonProperty("canAct")]
        public string CanAct { get; set; }

        [JsonProperty("hasReacted")]
        public bool HasReacted { get; set; }
    }

    public class HasActed
    {
        [JsonProperty("value")]
        public bool Value { get; set; }
    }

    public class OpenActionModule
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }
    }

    public class Amount
    {
        [JsonProperty("value")]
        public string Value { get; set; }
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
        public Profile By { get; set; }

        [JsonProperty("metadata")]
        public Metadata2 Metadata { get; set; }

        [JsonProperty("openActionModules")]
        public List<OpenActionModule> OpenActionModules { get; set; }

        [JsonProperty("operations")]
        public Operations Operations { get; set; }
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


    public class PublicationsRoot
    {
        [JsonProperty("data")]
        public PublicationsData Data { get; set; }
    }

    public class PublicationsData
    {
        [JsonProperty("publications")]
        public Publications Publications { get; set; }
    }

    public class Publications
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }
    }


    public class ProfilesManagedRoot
    {
        [JsonProperty("data")]
        public ProfilesManagedData Data { get; set; }

    }

    public class ProfilesManagedData
    {
        [JsonProperty("profilesManaged")]
        public ProfilesManaged ProfilesManaged { get; set; }
    }

    public class ProfilesManaged
    {
        [JsonProperty("items")]
        public List<Profile> Items { get; set; }
    }

    public class ChallengeRoot
    {
        [JsonProperty("data")]
        public ChallengeData Data { get; set; }
    }

    public class ChallengeData
    {
        [JsonProperty("challenge")]
        public Challenge Challenge { get; set; }
    }

    public class Challenge
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }


    public class AuthenticateRoot
    {
        [JsonProperty("data")]
        public AuthenticateData Data { get; set; }
    }

    public class AuthenticateData
    {
        [JsonProperty("authenticate")]
        public Authenticate Authenticate { get; set; }
    }

    public class Authenticate
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }


    public class ActOnOpenActionRoot
    {
        [JsonProperty("data")]
        public ActOnOpenActionData Data { get; set; }
    }

    public class ActOnOpenActionData
    {
        [JsonProperty("actOnOpenAction")]
        public ActOnOpenAction ActOnOpenAction { get; set; }
    }

    public class ActOnOpenAction
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("txHash")]
        public string TxHash { get; set; }

        [JsonProperty("txId")]
        public string TxId { get; set; }
    }

}
