using System;
namespace pa3_nwkostolni
{
    public class Post : IComparable<Post>
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return "Post " + Id + ": " + Message + " - " + Time;
        }

        public int CompareTo(Post temp)
        {
            return this.Id.CompareTo(temp.Id);
        }

        public static int CompareByTime(Post y, Post x)
        {
            return x.Time.CompareTo(y.Time);
        }
    }
}