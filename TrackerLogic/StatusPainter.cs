using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerLogic
{
    public static class StatusPainter
    {
        private const int PAYMENT_DUE_DATE_INDEX = 5;
        private const int PAYMENT_DATE_INDEX = 6;

        private static DateTime currentDate = DateTime.Parse(Clock.GetCurrentDateAndTime());
        private static DateTime paymentDueDate;
        private static DateTime paymentDate;
        private static bool isPaid = false;

        private static string status;
        private static Color backgroundColor;

        public static void PaintRow (DataGridViewRow rowToPaint)
        {
            paymentDueDate = (DateTime)rowToPaint.Cells[PAYMENT_DUE_DATE_INDEX].Value;

            if (rowToPaint.Cells[PAYMENT_DATE_INDEX].Value != null)
            {
                paymentDate = (DateTime)rowToPaint.Cells[PAYMENT_DATE_INDEX].Value;
                isPaid = true;
            }

            status = CheckStatus();
            SetColor(status);
            PaintCells(rowToPaint);
        }

        private static string CheckStatus ()
        {
            if (isPaid)
            {
                if (DateTime.Compare(paymentDate, paymentDueDate) >= 0)  // invoice paid in time
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
                if (DateTime.Compare(currentDate, paymentDueDate) == 1)  // overdue and unpaid
                {
                    return "overdue";
                }
                else if (paymentDueDate.Subtract(currentDate).Days <= 3)  // payment due time <= 3 days
                {
                    return "warning";
                }
                else
                {
                    return "default";
                }
            }
        }

        private static void SetColor (string type)
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

        private static void PaintCells (DataGridViewRow rowToPaint)
        {
            foreach (DataGridViewCell cell in rowToPaint.Cells)
            {
                cell.Style.BackColor = backgroundColor;
            }
        }
    }
}
