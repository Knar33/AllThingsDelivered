using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    //restaurant info
    public class Venue
    {
        public VenueResponse response { get; set; }
    }

    public class VenueResponse
    {
        public Venues[] venues { get; set; }
    }

    public class Venues
    {
        public bool hasMenu { get; set; }
        public string name { get; set; }
        public MenuUrl menu { get; set; }
        public VenueLocation location { get; set; }
        public VenueContact contact { get; set; }
        public string id { get; set; }
    }

    public class VenueContact
    {
        public string phone { get; set; }
    }

    public class VenueLocation
    {
        public string address { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Country { get; set; }
    }

    public class MenuUrl
    {
        public string url { get; set; }
    }

    //Restaurant menu

    public class Menu
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }

    public class Response
    {
        public Menus menu { get; set; }
    }

    public class Menus
    {
        public Provider provider { get; set; }
        public MenuList menus { get; set; }
    }

    public class Provider
    {
        public string name { get; set; }
        public string attributionImage { get; set; }
        public string attributionLink { get; set; }
        public string attributionText { get; set; }
    }

    public class MenuList
    {
        public int count { get; set; }
        public MenuItems[] items { get; set; }
    }

    public class MenuItems
    {
        public string menuId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public SectionEntries entries { get; set; }
    }

    public class SectionEntries
    {
        public int count { get; set; }
        public SectionItems[] items { get; set; }
    }

    public class SectionItems
    {
        public string sectionId { get; set; }
        public string name { get; set; }
        public ItemEntries entries { get; set; }
    }

    public class ItemEntries
    {
        public int count { get; set; }
        public ItemItems[] items { get; set; }
    }

    public class ItemItems
    {
        public string entryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string[] prices { get; set; }
        public string[] options { get; set; }
        public string[] additions { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
        public string requestId { get; set; }
    }
}