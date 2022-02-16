using System;
using System.ComponentModel;

namespace Domein
{
    public class Programma
    {
        private string _zender;

        public string Zender
        {
            get => _zender;
            set => _zender = value;
        }

        private string _uuid;

        public string Uuid
        {
            get => _uuid;
            set => _uuid = value;
        }

        private DateTime _from;

        public DateTime From
        {
            get => _from;
            set => _from = value;
        }

        private DateTime _to;

        public DateTime To
        {
            get => _to;
            set => _to = value;
        }

        private string _titel;

        public string Titel
        {
            get => _titel;
            set => _titel = value;
        }

        private bool _rerun;

        public bool Rerun
        {
            get => _rerun;
            set => _rerun = value;
        }

        private bool _live;

        public bool Live
        {
            get => _live;
            set => _live = value;
        }

        private string _beschrijving;

        public string Beschrijving
        {
            get => _beschrijving;
            set => _beschrijving = value;
        }

        private string _genre;

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        private int _duration;

        public int Duration
        {
            get => _duration;
            set => _duration = value;
        }

        private string _image;

        public string Image
        {
            get => _image;
            set => _image = value;
        }

        private string _imageFormat;

        public string ImageFormat
        {
            get => _imageFormat;
            set => _imageFormat = value;
        }

        private string _type;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        private ProductieLand _landVanAfkomst;

        public ProductieLand LandVanAfkomst
        {
            get => _landVanAfkomst;
            set => _landVanAfkomst = value;
        }

        public Programma(string zender, string uuid, DateTime from, DateTime to, string titel, bool rerun, bool live,
            string beschrijving, string genre, int duration, string image, string imageFormat, string type,
            ProductieLand landVanAfkomst)
        {
            Zender = zender;
            Uuid = uuid;
            From = from;
            To = to;
            Titel = titel;
            Rerun = rerun;
            Live = live;
            Beschrijving = beschrijving;
            Genre = genre;
            Duration = duration;
            Image = image;
            ImageFormat = imageFormat;
            Type = type;
            LandVanAfkomst = landVanAfkomst;
        }
        
        public Programma(){}

        public DateTime UnixToDate(long unixMiliseconden)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixMiliseconden);
            double unixTimeStampInSeconden = dateTimeOffset.ToUnixTimeSeconds();
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStampInSeconden)
                .ToLocalTime();
        }

        public bool IsAfgelopen()
        {
            return DateTime.Now > To;
        }
    }
}