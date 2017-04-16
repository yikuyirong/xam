using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;

using System;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcDJPage : UcZDPage
    {

        public event EventHandler UpdateComplete;

        protected HsLabelValue userData;

        public PageParams PP;

        #region isModified

        private bool _isModified = false;

        /// <summary>
        /// 指示是否单据数据是否变化。
        /// </summary>
        protected bool isModified
        {
            get { return _isModified; }
            set
            {
                _isModified = value;

                this.onCanExecuteChanged();
            }
        }

        #endregion


        protected int iNewRecode = 0;

        #region AuditOnly

        private bool _auditOnly = false;

        public bool AuditOnly
        {
            get { return _auditOnly; }
            set
            {
                _auditOnly = value;

                foreach (IControlValue control in this.controls)
                {
                    control.AllowEdit = !value;
                }
            }
        }

        #endregion

        #region Title

        private string _titleSaved = "Title";

        protected string titleSaved
        {
            get { return _titleSaved; }

            set
            {
                this._titleSaved = value;
                this.Title = value;
            }
        }

        protected string titleNotSaved
        {
            get
            {
                return titleSaved.EndsWith("*") ? titleSaved : titleSaved + "*";
            }
        }

        #endregion

        #region UniqueId

        private string _uniqueId = "-1";

        protected string uniqueId
        {
            get { return _uniqueId; }
            set
            {
                _uniqueId = value;

                if (this.imagePage != null)
                {
                    this.imagePage.DjId = value;
                }

                if (this.filePage != null)
                {
                    this.filePage.DjId = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// 更新只有是否关闭窗口。
        /// </summary>
        protected bool closeAfterUpdate = true;

        #region 附加页

        protected UcImageDetailPage imagePage;

        protected UcFileDetailPage filePage;

        #endregion

        #region 单据类型

        public string Djlx
        {
            set
            {
                if (imagePage != null)
                {
                    imagePage.Djlx = value;
                }

                if (filePage != null)
                {
                    filePage.Djlx = value;
                }
            }
        }

        #endregion

        public UcDJPage(HsLabelValue item = null) : base()
        {
            this.userData = item;
        }

        protected override void onInit()
        {
            base.onInit();

            #region 建立附件布局

            if (this.PP != null && this.PP.HasTab) //存在附件页
            {
                this.mainContent.Children.Insert(0, new UcHeaderTitle(this.PP.MainTitle));

                //加入图像页
                if (this.PP.HasImageTab) 
                {
                    imagePage = new UcImageDetailPage(PP.ImageTitle);

                    this.controls.Add(imagePage);

                    this.Children.Add(imagePage);
                }

                //加入文件页
                if (this.PP.HasFileTab)
                {
                    filePage = new UcFileDetailPage(PP.FileTitle);

                    this.controls.Add(filePage);

                    this.Children.Add(filePage);
                }

            }

            //页面切换时触发
            this.CurrentPageChanged += new EventHandler(async (sender, e) =>
            {
                try
                {
                    IUcDJAnnexPage page = this.CurrentPage as IUcDJAnnexPage;

                    if (page != null && !page.HasRetrieve)
                    {
                        await page.GetItems();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            });

            #endregion

            //
            this.AuditOnly = this.AuditOnly;

            //this.enterToolbarItem.Icon = "ion-checkmark-round";

            this.enterToolbarItem.Text = "保存";

            #region 为所有IControlValue添加事件

            foreach (IControlValue control in controls)
            {
                control.DataChanged += dataChangedEventHandler;
            }

            #endregion


            #region 单据状态初始化

            if (this.userData == null)
            {
                this.iNewRecode = 0;

                this.newData();

                this.setSaved();
            }
            else
            {
                this.iNewRecode = 1;

                this.setData(userData);

                this.setSaved();
            }

            #endregion
        }


        protected virtual void newData()
        {
            foreach (IControlValue control in this.controls)
            {
                control.Reset();
            }
        }

        protected abstract void setData(HsLabelValue data);

        protected void setSaved()
        {
            this.isModified = false;
            this.Title = this.titleSaved;
        }

        protected void dataChangedEventHandler(object sender, EventArgs e)
        {
            this.isModified = true;
            this.Title = this.titleNotSaved;
        }

        protected async override void callEnter()
        {
            try
            {
                this.validate();

                //更新主要信息
                string result = await this.update();

                //更新附件信息
                if (this.PP != null && this.PP.HasTab)
                {
                    //如果图片发生过变更则更新图片
                    if (this.imagePage != null && this.imagePage.HasDataChanged)
                    {
                        await this.imagePage.UpdateItems();
                    }

                    //如果文件发生过变更则更新文件
                    if (this.filePage != null && this.filePage.HasDataChanged)
                    {
                        await this.imagePage.UpdateItems();
                    }
                }

                this.ShowInformation(result);

                //
                this.UpdateComplete?.Invoke(this, null);

                if (closeAfterUpdate)
                {
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        #region ICommand

        public override bool CanExecute(object parameter)
        {
            return !AuditOnly && isModified;
        }

        #endregion

        public class PageParams
        {
            private string _mainTitle = "基本信息";

            private string _imageTitle = "图片附件";

            private bool _hasImageTab = true;

            private string _detailTitle = "单据明细";

            private bool _hasDetailTab = false;

            private string _fileTitle = "文件附件";

            private bool _hasFileTab = true;

            public bool HasTab
            {
                get
                {
                    return _hasImageTab || _hasFileTab || _hasDetailTab;
                }
            }

            public string MainTitle
            {
                get { return _mainTitle; }
                set { _mainTitle = value; }
            }

            public string ImageTitle
            {
                get { return _imageTitle; }
                set { _imageTitle = value; }
            }

            public bool HasImageTab
            {
                get { return _hasImageTab; }
                set { _hasImageTab = value; }
            }

            public string FileTitle
            {
                get { return _fileTitle; }
                set { _fileTitle = value; }
            }

            public bool HasFileTab
            {
                get { return _hasFileTab; }
                set { _hasFileTab = value; }
            }

            public string detailTitle
            {
                get { return _detailTitle; }
                set { _detailTitle = value; }
            }

            public bool HasDetailTab
            {
                get { return _hasDetailTab; }
                set { _hasDetailTab = value; }
            }
        }

    }
}
