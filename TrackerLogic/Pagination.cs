using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TrackerLogic
{
    public static class Pagination
    {
        private const int NUMBER_OF_RECORDS_PER_PAGE = 20;

        public static void Set (Dataset invoiceDataset, ref int currentDatasetSize, ref int numberOfPages, ref int lastPageIndex,
            List<BindingList<Invoice>> subsetsOfData)
        {
            subsetsOfData.Clear();
            currentDatasetSize = invoiceDataset.Contents.Count();
            if (currentDatasetSize == 0)
            {
                numberOfPages = 1;
            }
            else
            {
                if (currentDatasetSize % NUMBER_OF_RECORDS_PER_PAGE == 0)
                {
                    numberOfPages = currentDatasetSize / NUMBER_OF_RECORDS_PER_PAGE;
                }
                else
                {
                    numberOfPages = (currentDatasetSize / NUMBER_OF_RECORDS_PER_PAGE) + 1;
                }
            }
            lastPageIndex = numberOfPages - 1;

            for (int n = 0; n < numberOfPages; n++)
            {
                List<Invoice> newSubset =
                    invoiceDataset.Contents.Skip(n * NUMBER_OF_RECORDS_PER_PAGE).Take(NUMBER_OF_RECORDS_PER_PAGE).ToList();
                BindingList<Invoice> bindingSubset = new BindingList<Invoice>(newSubset);

                subsetsOfData.Add(bindingSubset);
            }
        }
    }
}
