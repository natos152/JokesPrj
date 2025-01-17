﻿using JokesPrj.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace JokesPrj.DAL
{
    public class UserDAL
    {
        private readonly string conStr;
        public UserDAL(string conStr)
        {
            this.conStr = conStr;
        }

        public User GetUserByID(int id_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    User u = null;
                    string query = $"SELECT * FROM JokesUsers where id_user= @id_user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_user", SqlDbType.Int).Value = id_user;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        u = new User(Convert.ToInt32(reader["id_user"]), Convert.ToString(reader["username"]), Convert.ToString(reader["phash"]), Convert.ToString(reader["email"]), Convert.ToString(reader["user_img"]), Convert.ToInt32(reader["i_follow"]), Convert.ToInt32(reader["follow_me"]), Convert.ToString(reader["id_external"]));
                    return u;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserByIDExternal(string id_external)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    User u = null;
                    string query = $"SELECT * FROM JokesUsers where id_external= @id_external";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_external", SqlDbType.NVarChar).Value = id_external;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        u = new User(Convert.ToInt32(reader["id_user"]), Convert.ToString(reader["username"]), Convert.ToString(reader["phash"]), Convert.ToString(reader["email"]), Convert.ToString(reader["user_img"]), Convert.ToInt32(reader["i_follow"]), Convert.ToInt32(reader["follow_me"]), Convert.ToString(reader["id_external"]));
                    return u;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUser(User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"SELECT * FROM JokesUsers where id_user= @id_user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_user", SqlDbType.Int).Value = user.Id_user;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        user = new User(Convert.ToInt32(reader["id_user"]), Convert.ToString(reader["username"]), Convert.ToString(reader["phash"]), Convert.ToString(reader["email"]), Convert.ToString(reader["user_img"]), Convert.ToInt32(reader["i_follow"]), Convert.ToInt32(reader["follow_me"]), Convert.ToString(reader["id_external"]));
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User UpdateUser(User new_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update JokesUsers Set username = '{new_user.Username}',phash = '{new_user.Hash}',email = '{new_user.Email}',salt = '{new_user.Salt}' where id_user= @id_user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_user", SqlDbType.Int).Value = new_user.Id_user;
                    cmd.ExecuteNonQuery();
                    int rows = UpdateUsernameOnComments(new_user);
                    rows += UpdateUsernameOnLikes(new_user);
                    rows += UpdateUsernameOnJokes(new_user);
                    rows += UpdateUsernameOnIFollow(new_user);
                    rows += UpdateUsernameOnFollowMe(new_user);
                    return new_user;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public int UpdateUsernameOnComments(User new_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update JokesComments Set username=@username where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = new_user.Username;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = new_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateUsernameOnLikes(User new_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update JokesLikes Set username=@username where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = new_user.Username;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = new_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateUsernameOnJokes(User new_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update Jokes Set username=@username where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = new_user.Username;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = new_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateUsernameOnIFollow(User new_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update Follow Set username=@username where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = new_user.Username;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = new_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateUsernameOnFollowMe(User new_user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update Follow Set target_username=@target_username where target_id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@target_username", SqlDbType.NVarChar).Value = new_user.Username;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = new_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int UpdateIFollow(User logged_user, bool status)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    if (status == true)
                    {
                        logged_user.I_follow += 1;
                    }
                    else
                    {
                        logged_user.I_follow -= 1;
                    }
                    string query = $"Update JokesUsers Set i_follow=@i_follow where id_user=@id_user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@i_follow", SqlDbType.NVarChar).Value = logged_user.I_follow;
                    cmd.Parameters.AddWithValue("@id_user", SqlDbType.Int).Value = logged_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateFollowMe(User target_user, bool status)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    if (status == true)
                    {
                        target_user.Follow_me += 1;
                    }
                    else
                    {
                        target_user.Follow_me -= 1;
                    }
                    string query = $"Update JokesUsers Set follow_me=@follow_me where id_user=@id_user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@follow_me", SqlDbType.NVarChar).Value = target_user.Follow_me;
                    cmd.Parameters.AddWithValue("@id_user", SqlDbType.Int).Value = target_user.Id_user;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserHash(User u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"SELECT * FROM JokesUsers where username= @username ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", u.Username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        u = new User(Convert.ToInt32(reader["id_user"]), Convert.ToString(reader["username"]), Convert.ToString(reader["phash"]), Convert.ToString(reader["email"]), Convert.ToString(reader["user_img"]), Convert.ToInt32(reader["i_follow"]), Convert.ToInt32(reader["follow_me"]), Convert.ToString(reader["salt"]), Convert.ToString(reader["id_external"]));
                    return u;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int SaveNewUserToDB(User u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Insert into JokesUsers (username,phash,email,user_img,i_follow,follow_me,salt,id_external) VALUES (@username,@phash,@email,@user_img,@i_follow,@follow_me,@salt,@id_external)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = u.Username;
                    cmd.Parameters.AddWithValue("@phash", SqlDbType.NVarChar).Value = u.Hash;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.NVarChar).Value = u.Email;
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = u.User_img;
                    cmd.Parameters.AddWithValue("@i_follow", SqlDbType.Int).Value = 0;
                    cmd.Parameters.AddWithValue("@follow_me", SqlDbType.Int).Value = 0;
                    cmd.Parameters.AddWithValue("@salt", SqlDbType.NVarChar).Value = u.Salt;
                    cmd.Parameters.AddWithValue("@id_external", SqlDbType.NVarChar).Value = u.Id_external;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int UpdateUserImageOnPosts(string path, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update Jokes Set user_img=@user_img where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = path;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateUserImageOnLikes(string path, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update JokesLikes Set user_img=@user_img where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = path;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateUserImageOnComments(string path, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update JokesComments Set user_img=@user_img where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = path;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateUserImageOnIFollow(string path, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update Follow Set user_img=@user_img where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = path;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateUserImageOnFollowMe(string path, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update Follow Set target_img=@target_img where target_id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@target_img", SqlDbType.NVarChar).Value = path;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SaveNewPhotoToDB(string path, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Update JokesUsers Set user_img=@user_img where id_user=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = path;
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    int rows = UpdateUserImageOnPosts(path, id);
                    rows = UpdateUserImageOnPosts(path, id);
                    rows = UpdateUserImageOnLikes(path, id);
                    rows = UpdateUserImageOnComments(path, id);
                    rows = UpdateUserImageOnIFollow(path, id);
                    rows = UpdateUserImageOnFollowMe(path, id);
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}