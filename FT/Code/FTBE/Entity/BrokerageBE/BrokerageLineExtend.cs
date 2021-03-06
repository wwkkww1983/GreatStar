﻿

#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UFIDA.U9.SM.SO;
using UFSoft.UBF.PL;
using UFSoft.UBF.Business;

#endregion

namespace UFIDA.U9.Cust.GS.FT.BrokerageBE {

	public partial class BrokerageLine{

		#region Custom Constructor

		#endregion

		#region before & after CUD V 
		/*	实体提交的事件顺序示例(QHELP) 主实体A 组合 非主实体B .
		/ (新增A和B两个实例)A.OnSetDefaultValue->B.OnSetDefaultValue-> B.OnValidate ->A.OnValidate ->A.OnInserting ->B.OnInserting ->产生提交SQL ->B.OnInserted ->A.OnInserted
		/ (仅修改B,A也会被修改))A.OnSetDefaultValue->B.OnSetDefaultValue-> B.OnValidate ->A.OnValidate ->A.OnUpdating ->B.OnUpdating ->产生提交SQL ->B.OnUpdated ->A.OnUpdated
		/ (删除A,B会被级联删除))A.OnDeleting ->B.OnDeleting ->产生提交SQL ->B.OnDeleted ->A.OnDeleted
		/	产生提交SQL顺序则看其外键，增修一对多先A后B，一对一先B后A。　删除一对多先B后A，一对一先A后B .
		*/	
		/// <summary>
		/// 设置默认值
		/// </summary>
		protected override void OnSetDefaultValue()
		{
			base.OnSetDefaultValue();
		}
		/// <summary>
		/// before Insert
		/// </summary>
		protected override void OnInserting() {
			base.OnInserting();
			// TO DO: write your business code here...
		}

		/// <summary>
		/// after Insert
		/// </summary>
		protected override void OnInserted() {
			base.OnInserted();
			// TO DO: write your business code here...
		}

		/// <summary>
		/// before Update
		/// </summary>
		protected override void OnUpdating() {
			base.OnUpdating();
			// TO DO: write your business code here...
		}

		/// <summary>
		/// after Update
		/// </summary>
		protected override void OnUpdated() {
			base.OnUpdated();
			// TO DO: write your business code here...
		}


		/// <summary>
		/// before Delete
		/// </summary>
		protected override void OnDeleting() {
			base.OnDeleting();
			// TO DO: write your business code here...
		}

		/// <summary>
		/// after Delete
		/// </summary>
		protected override void OnDeleted() {
			base.OnDeleted();
			// TO DO: write your business code here...
		}

		/// <summary>
		/// on Validate
		/// </summary>
		protected override void OnValidate() {
			base.OnValidate();
			this.SelfEntityValidator();
            if (this.PayMan == null) 
            {
                throw new BusinessException(string.Format("行【{0}】对应的收款人为空，请确认！", this.RowID));
            }
            if (this.BrokerageType == AllEnumBE.DiscountTypeEnum.FixedValues && this.Currenty == null)
                throw new BusinessException("当佣金方式为固定值时，币种不允许为空！");
			// TO DO: write your business code here...
		}
		#endregion
		
		#region 异常处理，开发人员可以重新封装异常
		public override void  DealException(Exception e)
        	{
          		base.DealException(e);
          		throw e;
        	}
		#endregion

		#region  扩展属性代码区
		
		#endregion

		#region CreateDefault
		
		private  static BrokerageLine CreateDefault_Extend(UFIDA.U9.Cust.GS.FT.BrokerageBE.BrokerageHead parentEntity) {
			//TODO delete next code and add your custome code here
			throw new NotImplementedException () ;
		}
	    		
		#endregion 






        #region Model Methods

        public static BrokerageLine.EntityList GetBrokerageLineBySOLine(SOLine soline)
        {
            if (soline != null)
            {
                SO so = soline.SO;

                if (so != null)
                {
                    string strWhere = "BrokerageHead.Custmer.Code=@Customer and BrokerageHead.Product.Code=@Item and BrokerageHead.States=2 and BrokerageHead.Org=@Org and Valid=1 and @Date >= ValidDate and @Date <= UnValidDate";
                    OqlParam[] appOqlparm = new OqlParam[] {
                            new OqlParam("Customer", soline.SO.OrderBy.Code),
                            new OqlParam("Item", soline.ItemInfo.ItemCode),
                            new OqlParam("Org",soline.SO.Org.ID),
                            new OqlParam("Date",soline.SO.BusinessDate)
                        };
                    UFIDA.U9.Cust.GS.FT.BrokerageBE.BrokerageLine.EntityList list = UFIDA.U9.Cust.GS.FT.BrokerageBE.BrokerageLine.Finder.FindAll(strWhere, appOqlparm);

                    return list;
                }
            }
            return null;
        }
		#endregion		
	}
}
