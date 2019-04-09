﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace UploadManager
{
    public static class UploadManager
    {
        #region 属性

        //private const string FilesSubdir = "attachments";
        private const string TempExtension = "";

        /// <summary>
        /// 上传到服务器的物理路径
        /// </summary>
        public static string UploadFolderPhysicalPath { get; set; }

        /// <summary>
        /// 上传到服务器的相对路径
        /// </summary>
        public static string UploadFolderRelativePath { get; set; }


        #endregion

        #region Ctor

        static UploadManager()
        {
            //从配置文件中获取上传文件夹
            if (String.IsNullOrWhiteSpace(WebConfigurationManager.AppSettings["UploadFolder"]))
            {
                UploadFolderRelativePath = @"~/upload";
                UploadFolderPhysicalPath = HostingEnvironment.MapPath(UploadFolderRelativePath);
            }

            else
            {
                UploadFolderRelativePath = WebConfigurationManager.AppSettings["UploadFolder"];
                UploadFolderPhysicalPath = UploadFolderRelativePath;
            }



            if (!Directory.Exists(UploadFolderPhysicalPath))
                Directory.CreateDirectory(UploadFolderPhysicalPath);//如果第一级路径不存在则创建目录
        }

        #endregion

        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="str1">路径拼接1</param>
        /// <param name="str2">路径拼接2</param>
        /// <param name="str3">路径拼接3</param>
        /// <param name="str4">文件类型</param>
        /// <returns>存储的路径</returns>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static String SaveFile(HttpPostedFileBase MyFile, string str1, string str2, string str3, string str4,string fileName)
        {
            string tempPath = string.Empty, targetPath = string.Empty;

            try
            {


                if (str2 != null)
                {
                    

                    tempPath = GetTempFilePath(fileName);//获取临时文件路径
                    targetPath = GetTargetFilePath(str1, str2, str3, str4,fileName);//获取目标文件路径("盘符:\\\\存放的最上层文件夹名字\\店铺名字\\提交案ID\\文件类型\\日期\\文件")


                    //若上传文件夹中子文件夹未存在则创建
                    var file = new FileInfo(targetPath);
                    var tempFile = new FileInfo(tempPath);
                    if (file.Directory != null && !file.Directory.Exists)
                    {
                        file.Directory.Create();

                    }
                    if (tempFile.Directory != null && !tempFile.Directory.Exists)
                    {
                        tempFile.Directory.Create();
                    }

                    //FileStream fs = File.Open(tempPath, FileMode.Append);

                    if (MyFile.ContentLength > 0)
                    {
                        //SaveFile(stream, fs);
                        MyFile.SaveAs(tempPath);
                    }
                    //fs.Close();

                    if (File.Exists(targetPath))
                    { //如果文件存在，直接替换文件，并建立备份
                        File.Replace(tempPath, targetPath, targetPath + ".bak");
                    }
                    else
                    {
                        //上传完毕将临时文件移动到目标文件
                        File.Move(tempPath, targetPath);
                    }

                }
            }
            catch (Exception e)
            {
                // 若上传出错，则删除上传到文件夹文件
                if (File.Exists(targetPath))
                    File.Delete(targetPath);

                //// 删除临时文件
                //if (File.Exists(tempPath))
                //    File.Delete(tempPath);

                return null;
            }
            finally
            {
                // 删除临时文件
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
            return targetPath;
        }

        /// <summary>
        /// 循环读取流到文件流中
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fs"></param>
        //public static void SaveFile(Stream stream, FileStream fs)
        //{
        //    var buffer = new byte[4096];
        //    int bytesRead;
        //    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        //    {
        //        fs.Write(buffer, 0, bytesRead);
        //    }
        //}

        #endregion

        #region 获取临时文件夹路径

        public static string GetTempFilePath(string fileName)
        {
            fileName = fileName + TempExtension;
            //Path.Combine(@HostingEnvironment.ApplicationPhysicalPath, Path.Combine(UploadFolderPhysicalPath, fileName));
            return Path.Combine(UploadFolderPhysicalPath, "temp", DateTime.Now.Date.ToString("yyyy-MM-dd"), fileName);
        }

        /// <summary>
        /// 目标文件夹路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="culture"></param>
        /// <param name="optionalSubdir"></param>
        /// <returns></returns>
        public static string GetTargetFilePath(string str1, string str2, string str3, string str4,string fileName)
        {
            return Path.Combine(UploadFolderPhysicalPath, str1, str2, str3,str4,fileName);
        }

        #region 依据路径删除文件

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        #endregion

        #endregion
    }
}
