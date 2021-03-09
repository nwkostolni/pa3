using System.IO;
using System.Collections.Generic;
using System;

namespace pa3_nwkostolni
{
    public class PostFile
    {
        public static List<Post> GetPosts()
            {
                List<Post> socialPosts = new List<Post>();
                StreamReader inFile = null;
                

                try
                {
                    inFile = new StreamReader("posts.txt");
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Something went wrong! Returning blank list. {0}", e);
                    return socialPosts;
                }

                string line = inFile.ReadLine(); //priming read

                while (line != null)
                {
                    string[] temp = line.Split("#");
                    int IdTemp = int.Parse(temp[0]);
                    int TimeTemp = int.Parse(temp[1]);
                    socialPosts.Add(new Post(){Id = IdTemp, Time = TimeTemp, Message = temp[2]});
                    line = inFile.ReadLine(); //update read
                }

                inFile.Close();


                return socialPosts;
            }
        }
    }
