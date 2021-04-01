#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EditViewModel
// 创 建 者：杨程
// 创建时间：2021/3/29 16:51:07
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
using System.Windows;
using System.Windows.Controls;
using DarkNote.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace DarkNote.ViewModels
{
    public class EditViewModel:ViewModelBase
    {
        public EditViewModel()
        {
            //构造函数
            //RichTextBox rt;
            //rt.
            
            
        }

        #region 属性
        
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        
        #endregion

        #region 命令
        public RelayCommand<Window> DingDing => new RelayCommand<Window>((w)=> 
        {

            
        });

        public RelayCommand<Window> CloseNoteCommand => new RelayCommand<Window>((w) =>
          {
              w.Close();
          });
        #endregion
    }
}
