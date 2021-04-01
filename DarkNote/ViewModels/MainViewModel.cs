#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：MainViewModel
// 创 建 者：杨程
// 创建时间：2021/3/29 12:51:42
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//
//
//----------------------------------------------------------------*/

#endregion << 文 件 说 明 >>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DarkNote.Common;
using DarkNote.Model;
using DarkNote.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;

namespace DarkNote.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            NoteList = new ObservableCollection<NoteModel>();
            //构造函数
            LoadNote(true);

            #region 测试数据--作废

            //NoteList.Add(new NoteModel()
            //{
            //    GuidId = Guid.NewGuid().ToString(),
            //    NoteTitle = "1",
            //    NoteDate = "2021年3月29日",
            //    NoteBackground = "#f15b6c",
            //    NoteText = "hahahahahahhahahahahaha/r/n蔷薇蔷薇蔷薇蔷薇蔷薇"
            //});
            //NoteList.Add(new NoteModel()
            //{
            //    GuidId = Guid.NewGuid().ToString(),
            //    NoteTitle = "2",
            //    NoteDate = "2021年3月28日",
            //    NoteBackground = "#2e3a1f",
            //    NoteText = "hahahahahahhahahahahaha/r/n蔷薇蔷薇蔷薇蔷薇蔷薇"
            //});
            //NoteList.Add(new NoteModel()
            //{
            //    GuidId = Guid.NewGuid().ToString(),
            //    NoteTitle = "3",
            //    NoteDate = "2021年3月27日",
            //    NoteBackground = "#4e72b8",
            //    NoteText = "hahahahahahhahahahahaha/r/n蔷薇蔷薇蔷薇蔷薇蔷薇"
            //});

            #endregion 测试数据--作废

            Messenger.Default.Register<bool>(this, "LoadNote", LoadNote);
        }

        #region 属性

        public ObservableCollection<NoteModel> NoteList { get; set; }

        public string Title { get; set; } = "DarkNote便签";

        private Dictionary<string, string> FilePaths = new Dictionary<string, string>();


        public string SearchText { get; set; }

        #endregion 属性

        #region 私有方法

        private void LoadNote(bool isLoad)
        {
            SearchText = "";
            NoteList.Clear();
            FilePaths.Clear();
            List<NoteModel> currentList = new List<NoteModel>();

            //便签存储路径
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}Notes";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            //获取文件夹内的json文件
            string[] files = Directory.GetFiles(filePath);

            //使用foreach赋值
            foreach (var file in files)
            {
                var current = File.ReadAllText(file);
                NoteModel nm = JsonConvert.DeserializeObject<NoteModel>(current);
                currentList.Add(nm);
                //NoteList.Add(nm);
                FilePaths.Add(nm.GuidId, file);
            }
            currentList.Sort((left, right) =>
            {
                if (Convert.ToDateTime(left.NoteDate) > Convert.ToDateTime(right.NoteDate))
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });

            for (int i = 0; i < currentList.Count; i++)
            {
                NoteList.Add(currentList[i]);
            }
            //NoteList = new ObservableCollection<NoteModel>(currentList);
        }

        private List<NoteModel> GetListByObservableCollection(ObservableCollection<NoteModel> obs)
        {
            List<NoteModel> current = new List<NoteModel>();
            for (int i = 0; i < obs.Count; i++)
            {
                current.Add(obs[i]);
            }
            return current;
        }

        #endregion 私有方法

        #region 命令

        public RelayCommand<Window> CloseWindowCommand { get; set; } = new RelayCommand<Window>((w) =>
          {
              //w.Close();
              Application.Current.Shutdown();
          });

        public RelayCommand<Window> MinWindowCommand => new RelayCommand<Window>((w) => { w.WindowState = WindowState.Minimized; });

        public RelayCommand OpenNewNote => new RelayCommand(() =>
        {
            NoteModel noteModel = new NoteModel()
            {
                GuidId = Guid.NewGuid().ToString(),
                NoteDate = DateTime.Now.ToString(),
                NoteTitle = "",
                NoteText = ""
            };
            NoteWindowManager.Instance.CreateNoteWindow(noteModel);
        });

        public RelayCommand<NoteModel> OpenNote { get; set; } = new RelayCommand<NoteModel>((n) =>
         {
             NoteWindowManager.Instance.GetNoteWindow(n);
             //MessageBox.Show($"ID:{n.GuidId}\r\n背景颜色:{n.NoteBackground}\r\n文本:{n.NoteText}");
         });

        public RelayCommand<NoteModel> DeleteNote => new RelayCommand<NoteModel>((n) =>
        {
            //string FileGuid = n.GuidId;
            DialogWindow.Show("删除成功！", MessageType.Successful, NoteWindowManager.Instance.MainWindow);
            string FilePath = FilePaths[n.GuidId];
            File.Delete(FilePath);
            LoadNote(true);
        });

        public RelayCommand<string> SearchNoteCommand => new RelayCommand<string>((s) =>
          {
              //MessageBox.Show(s);
              if (string.IsNullOrEmpty(s))
              {
                  LoadNote(true);
              }
              else
              {
                  LoadNote(true);
                  List<NoteModel> current = GetListByObservableCollection(NoteList);

                  NoteList.Clear();

                  var aa = current.Where(w => w.NoteText.Contains(s)).ToList();
                  if (aa.Count > 0)
                  {
                      for (int i = 0; i < aa.Count; i++)
                      {
                          NoteList.Add(aa[i]);
                      }
                  }
              }
          });

        #endregion 命令
    }
}