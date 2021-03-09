using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using pa3_nwkostolni.Database;

namespace pa3_nwkostolni
{
    class Program
    {
        static void Main(string[] args)
        {

           
            Menu();
            Console.ReadKey();

    
            
        }

        static void PrintList()
        {

            IReadAllData readObject = new ReadData();
            List<Post> allPosts = readObject.GetAllPosts();

            allPosts.Sort(Post.CompareByTime);

            foreach(Post post in allPosts)
            {
                Console.WriteLine(post.Id + ": " + post.Message + " - "+ post.Time);
            }

           

        }

        static void Menu()
        {
            // 1. Add Post
            // 2. Delete Post
            // 3. Edit Post
            // 4. Reseed Data
            // 5. Show All Posts
            // 6. Exit
            
            string menuChoice = GetMenuChoice();
            Console.WriteLine();

            while (menuChoice != "6")
            {
                if (menuChoice == "1")
                {
                    Console.WriteLine("Add New Post");
                    Console.WriteLine();
                    PostAdd();
                    return;
                }

                if (menuChoice == "2")
                {
                    Console.WriteLine("Delete Post");
                    Console.WriteLine();
                    PostDelete();
                    return;
                }

                if (menuChoice == "3")
                {
                    Console.WriteLine("Edit Post");
                    Console.WriteLine();
                    PostEdit();
                    return;
                }

                if (menuChoice == "4")
                {
                    Console.WriteLine("Reseed Data");
                    Console.WriteLine();
                    SaveData saveObject = new SaveData();
                    saveObject.SeedData();
                    PrintList();
                    Menu();
                    return;
                }

                if (menuChoice == "5")
                {
                    Console.WriteLine("Show All Posts");
                    Console.WriteLine();
                    PrintList();
                    Menu();
                    return;
                }

                
            }

        }

        static string GetMenuChoice()
        {
            // Prompts the user for a valid input
            Console.WriteLine("Select a Function:\n" +
                "1. Add New Post\n" +
                "2. Delete Post\n" +
                "3. Edit Post\n" +
                "4. Reseed Data\n" +
                "5. Show All Posts\n"+
                "6. Exit");
            string userInput = Console.ReadLine();

            while (!(userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4" || userInput == "5" || userInput == "6"))
            {
                Console.WriteLine("Please Enter a Valid Choice:");
                userInput = Console.ReadLine();
            }
            return userInput;
        }

         static void PostAdd()
        {   

            PrintList();
            int newTime = 0;
            string newMessage = "";
            Console.WriteLine("\nEnter Post Time:");
            newTime = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Post Message:");
            newMessage = (Console.ReadLine());


            IReadAllData readObject = new ReadData();
            List<Post> allPosts = readObject.GetAllPosts();
        
            string cs = @"URI=file:c:\source\repos\321\pa3-nwkostolni\pa3-nwkostolni\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"INSERT INTO posts(message, time) VALUES(@message, @time)";
            cmd.Parameters.AddWithValue("@message", newMessage);
            cmd.Parameters.AddWithValue("@time", newTime);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            allPosts.Add(new Post { Time = newTime, Message = newMessage });
            Console.WriteLine("Saving Post");
            PrintList();
            Console.WriteLine();
            Menu();
        }
    
         static void PostDelete()
        {

            PrintList();
            var deleteId = 0;
            Console.WriteLine("\nEnter Post Id To Delete:");
            deleteId = int.Parse(Console.ReadLine());
            IReadAllData readObject = new ReadData();
            List<Post> allPosts = readObject.GetAllPosts();
            string cs = @"URI=file:c:\source\repos\321\pa3-nwkostolni\pa3-nwkostolni\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"DELETE FROM posts WHERE id = @id";
            cmd.Parameters.AddWithValue("@id" , deleteId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            allPosts.RemoveAll(x => x.Id == deleteId);
            Console.WriteLine("Deleting Post");
            PrintList();
            Menu();
        }
    
         static void PostEdit()
        {
            PrintList();
            int oldId;
            int editTime;
            string editMessage;
            SaveData saveObject = new SaveData();
            Console.WriteLine("Enter Post ID To Edit");
            oldId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Edited Post Time:");
            editTime = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Edited Post Message:");
            editMessage = (Console.ReadLine());
            IReadAllData readObject = new ReadData();
            List<Post> allPosts = readObject.GetAllPosts();
            string cs = @"URI=file:c:\source\repos\321\pa3-nwkostolni\pa3-nwkostolni\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            
            int index = allPosts.FindIndex(x => x.Id == oldId);
            allPosts[index].Time = editTime;
            allPosts[index].Message = editMessage;
            saveObject.SavePost(allPosts[index]);
            Console.WriteLine("Saving Edits\n");
            PrintList();
            Console.WriteLine();
            Menu();
            
        }


    }


}