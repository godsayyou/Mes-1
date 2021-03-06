﻿using Mes.BE.Objects;
using MES.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mes.BE.Services
{
    public partial class ServiceBE : IServiceBE
    {
        public ProductComponent GetProductComponent(Sender sender, Int32 ComponentID)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    ProductComponent obj = new ProductComponent();
                    obj.ComponentID = ComponentID;
                    if (op.LoadProductComponent(obj) == 0)
                        return null;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }
        public ProductComponent GetProductComponentByComponentTypeID(Sender sender, int ComponentTypeID, string ProductCode)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    ProductComponent obj = new ProductComponent();
                    obj.ComponentTypeID = ComponentTypeID;
                    obj.ProductCode = ProductCode;
                    if (op.LoadProductComponentByComponentTypeID(obj) == 0)
                        return null;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }
        public List<ProductComponent> GetAllProductComponents(Sender sender)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    return op.LoadProductComponents();
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }

        public List<ProductComponent> GetProductComponentByComponentID(Sender sender, Int32 ComponentID)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    ProductComponent obj = new ProductComponent();
                    obj.ComponentID = ComponentID;
                    return op.LoadProductComponentByComponentID(obj);
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }
        public List<ProductComponent> GetProductComponentByOrderID(Sender sender, Guid OrderID)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    return op.LoadProductComponentByOrderID(OrderID);
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }
        public List<ProductComponent> GetProductComponentByProductCode(Sender sender, string productCode)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    return op.LoadProductComponentByProductCode(productCode);
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }
        public SearchResult SearchProductComponent(Sender sender, SearchProductComponentArgs args)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy())
                {
                    return op.SearchProductComponent(args);
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }

        public void SaveProductComponent(Sender sender, ProductComponent obj)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy(true))
                {
                    if (op.LoadProductComponent(obj) == 0)
                    {
                        obj.Created = DateTime.Now;
                        obj.CreatedBy = sender.UserCode + "." + sender.UserName;
                        obj.Modified = DateTime.Now;
                        obj.ModifiedBy = sender.UserCode + "." + sender.UserName;
                        op.InsertProductComponent(obj);
                    }
                    else
                    {
                        obj.Modified = DateTime.Now;
                        obj.ModifiedBy = sender.UserCode + "." + sender.UserName;
                        op.UpdateProductComponentByComponentID(obj);
                    }
                    op.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }

        public void SaveProductComponents(Sender sender, SaveProductComponentArgs args)
        {
            try
            {
                using (ObjectProxy op = new ObjectProxy(true))
                {
                    if (args.ProductComponents != null)
                    {
                        foreach (ProductComponent Item in args.ProductComponents)
                        {
                            Item.Created = DateTime.Now;
                            Item.CreatedBy = sender.UserCode + "." + sender.UserName;
                            Item.Modified = DateTime.Now;
                            Item.ModifiedBy = sender.UserCode + "." + sender.UserName;
                            if (op.LoadProductComponent(Item) == 0)
                            {
                                op.InsertProductComponent(Item);
                            }
                            else
                            {
                                op.UpdateProductComponentByComponentID(Item);
                            }
                        }
                    }

                    op.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                PLogger.LogError(ex);
                throw ex;
            }
        }
    }
}