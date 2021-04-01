#region<<文 件 说 明>>
/*----------------------------------------------------------------
// 文件名称：ViewModelLocator
// 创 建 者：杨程
// 创建时间：2021/3/8 星期一 11:07:22
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkNote.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace DarkNote.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //构造函数
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditViewModel>();
        }

        #region 返回项
        /// <summary>
        /// 返回MainViewModel
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// 返回MainViewModel
        /// </summary>
        public EditViewModel EditViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<EditViewModel>();
            }
        }


        #endregion
    }
}