using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project
{
    class Filehandler
    {
        public static string userFile = "Users.txt";

        // READ USERS FROM FILE
        public List<User> ReadUsers()
        {
            List<User> users = new List<User>();
            StreamReader read = null;

            try
            {
                if (File.Exists(userFile))
                {
                    FileStream inFile = new FileStream(userFile, FileMode.Open, FileAccess.Read);
                    read = new StreamReader(inFile);
                    string user = "";

                    while (!read.EndOfStream)
                    {
                        user = read.ReadLine();
                        string[] column = user.Split(':');
                        users.Add(new User(column[0], column[1], column[2], column[3]));
                    }
                }
                else
                {
                    throw new FileNotFoundException("File not found");
                }
            }
            catch (FileNotFoundException noFile)
            {
                MessageBox.Show(noFile.Message, "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (read != null)
                {
                    read.Close();
                }
            }
            return users;
        }

        // REGISTER NEW USER
        public void WriteUsers(string userDetails)
        {
            FileStream outFile = new FileStream(userFile, FileMode.Append, FileAccess.Write);
            StreamWriter write = new StreamWriter(outFile);

            write.WriteLine(userDetails);

            write.Close();
            outFile.Close();
        }
    }
}
