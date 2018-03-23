using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerLogic
{
    public static class StatusPainter
    {
        private const int PAYMENT_DUE_DATE_INDEX = 5;
        private const int PAYMENT_DATE_INDEX = 6;

        public static void PaintRow(DataGridViewRow rowToPaint)
        {
            DateTime currentDate = DateTime.Now;
            DateTime paymentDueDate = DateTime.MinValue;
            DateTime paymentDate = DateTime.MinValue;
            bool isPaid = false;

            string status = "default";
            Color backgroundColor = Color.White;

            paymentDueDate = (DateTime)rowToPaint.Cells[PAYMENT_DUE_DATE_INDEX].Value;

            if ((DateTime)rowToPaint.Cells[PAYMENT_DATE_INDEX].Value != Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paymentDate = (DateTime)rowToPaint.Cells[PAYMENT_DATE_INDEX].Value;
                isPaid = true;
            }

            status = CheckStatus(isPaid, currentDate, paymentDueDate, paymentDate);
            SetColor(status, ref backgroundColor);
            PaintCells(rowToPaint, backgroundColor);
        }

        private static string CheckStatus(bool isPaid, DateTime currentDate, DateTime paymentDueDate, DateTime paymentDate)
        {
            if (isPaid)
            {
                if (DateTime.Compare(paymentDate, paymentDueDate) <= 0)  // invoice paid in time
                {
                    return "ok";
                }
                else
                {
                    return "paidOverdue";
                }
            }
            else
            {
                // for unpaid invoices
                if ((DateTime.Compare(currentDate, paymentDueDate) == 1) && (paymentDueDate.Subtract(currentDate).Days < 0))
                {
                    return "overdue";
                }
                else if (paymentDueDate.Subtract(currentDate).Days <= 3)
                {
                    return "warning";
                }
                else
                {
                    return "default";
                }
            }
        }

        private static void SetColor(string type, ref Color backgroundColor)
        {
            switch (type)
            {
                case "ok":
                    // invoice paid in time, paint the row light-green
                    backgroundColor = Color.FromArgb(141, 228, 126);
                    break;

                case "warning":
                    // payment due date in <= 3 days, paint the row yellow
                    backgroundColor = Color.FromArgb(255, 255, 66);
                    break;

                case "overdue":
                    // invoice unpaid and overdue, paint it red
                    backgroundColor = Color.FromArgb(244, 57, 57);
                    break;

                case "paidOverdue":
                    // invoice paid over the deadline, paint it grey
                    backgroundColor = Color.FromArgb(195, 195, 195);
                    break;

                case "default":
                    // black on white, default state
                    backgroundColor = Color.White;
                    break;
            }
        }

        private static void PaintCells(DataGridViewRow rowToPaint, Color backgroundColor)
        {
            foreach (DataGridViewCell cell in rowToPaint.Cells)
            {
                cell.Style.BackColor = backgroundColor;
            }
        }
    }
}
