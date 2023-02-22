using BusinessObject;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataAccess
{
    public class OrderDAO : IOrderRepository
    {
        public ASS1_DBContext db = new ASS1_DBContext();

        public void addOrder(Order order)
        {
            db.Orders.Add(order);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Add successfully!");
            }
        }

        public void deleteOrder(Order order)
        {
            db.Remove<Order>(order);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Record removed successfully");
            }
        }

        public void editOrder(Order order)
        {
            db.Update<Order>(order);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Edit success.");
            }
            else
            {
                MessageBox.Show("Edit failed.");
            }
        }
    }
}
