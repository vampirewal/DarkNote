#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：NoteModel
// 创 建 者：杨程
// 创建时间：2021/3/29 11:23:56
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace DarkNote.Model
{
    /// <summary>
    /// NoteModel类
    /// </summary>
    public class NoteModel: ObservableObject
    {
        public NoteModel()
        {
            NoteBackground = "#20232A";
        }


        private string _GuidId;

        public string GuidId
        {
            get { return _GuidId; }
            set { _GuidId = value; RaisePropertyChanged(); }
        }


        private string noteTitle;
        /// <summary>
        /// 标签标题
        /// </summary>
        public string NoteTitle
        {
            get { return noteTitle; }
            set { noteTitle = value; RaisePropertyChanged(); }
        }

        private string noteDate;

        public string NoteDate
        {
            get { return noteDate; }
            set { noteDate = value; RaisePropertyChanged(); }
        }

        private string noteBackground;

        public string NoteBackground
        {
            get { return noteBackground; }
            set { noteBackground = value; RaisePropertyChanged(); }
        }

        private string noteText;

        public string NoteText
        {
            get { return noteText; }
            set { noteText = value; RaisePropertyChanged(); }
        }

    }
}
