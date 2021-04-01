using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DarkNote.Common;
using DarkNote.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;

namespace DarkNote.Views
{
    /// <summary>
    /// EditView.xaml 的交互逻辑
    /// </summary>
    public partial class EditView : Window , INotifyPropertyChanged
    {
        public EditView(NoteModel note)
        {
            InitializeComponent();
            this.DataContext = this;
            noteModel = note;

            //TextBox tb;
            //tb.

            OldText = note.NoteText;
            //OldNoteModel = note;
            OldBackground = note.NoteBackground;
        }

        #region 属性

        private NoteModel _notemodel;

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        } 
        #endregion

        public NoteModel noteModel
        {
            get => _notemodel;
            set
            {
                _notemodel = value;
                DoNotify();
            }
        }

        public string Title { get; set; } = "NOTE";

        public bool IsDing { get; set; } = false;

        private readonly string OldText;

        private readonly string OldBackground;

        #endregion 属性

        #region 命令

        public RelayCommand<Window> DingDing => new RelayCommand<Window>((w) =>
        {
            IsDing = !IsDing;
            if (IsDing)
            {
                w.Topmost = true;
            }
            else
            {
                w.Topmost = false;
            }
        });

        public RelayCommand<Window> CloseNoteCommand => new RelayCommand<Window>((w) =>
        {
            SaveNote();
            //w.Close();
            NoteWindowManager.Instance.CloseNoteWindow(noteModel);
        });

        public RelayCommand SaveNoteCommand => new RelayCommand(() =>
        {
            SaveNote();
        });

        private void SaveNote()
        {
            if (!OldText.Equals(noteModel.NoteText)||!OldBackground.Equals(noteModel.NoteBackground))
            {
                noteModel.NoteDate = DateTime.Now.ToString();
                string Path = $"{AppDomain.CurrentDomain.BaseDirectory}Notes";
                string filePath = Path + @"\" + noteModel.GuidId + ".json";
                var json = JsonConvert.SerializeObject(noteModel);
                var formatStr = JsonHelper.FormatJsonString(json);
                FileStream fs = File.Create(filePath);
                fs.Dispose();
                File.AppendAllText(filePath, formatStr);

                
            }
            Messenger.Default.Send<bool>(true, "LoadNote");
            DialogWindow.Show("保存NOTE成功！", MessageType.Successful, NoteWindowManager.Instance.MainWindow);
        }


        public RelayCommand<string> SelectColor => new RelayCommand<string>((s) =>
          {
              noteModel.NoteBackground = s;
          });
        #endregion 命令

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsDing)
            {
                this.DragMove();
            }
            
        }
    }
}