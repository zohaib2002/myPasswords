using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace myPasswords
{
    class entry
    {
        public string group, title, username, email, password, u_r_l, notes;

        public static entry get_record(string record_string) 
        {
            string[] entities = record_string.Split('\t');
            entry Entry = new entry(entities[0], entities[1], entities[2], entities[3], entities[4], entities[5], entities[6]);

            return Entry;

        }

        public entry(string group, string title, string username, string email, string password, string u_r_l, string notes)
        {
            this.group = group;
            this.title = title;
            this.username = username;
            this.email = email;
            this.password = password;
            this.u_r_l = u_r_l;
            this.notes = notes;
        }

        public string return_record() 
        {
            return this.group + '\t' + this.title + '\t' + this.username + '\t' + this.email + '\t'
                + this.password + '\t' + this.u_r_l + '\t' + this.notes + '\n';
        }
    }
}
