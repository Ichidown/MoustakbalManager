using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoustakbalManager.Tools
{
	class Pagination
	{
		public static int[] GetPaginationArray(int currentPage, int pagesCount,int pageSideSteps)//int currentPage, int pagesCount, int pageSideSteps)
		{
			int length = (3 + (pageSideSteps * 2) > pagesCount)? // if all pages can fit
				pagesCount : 
				3 + pageSideSteps * 2; // 3 for : current page + 1st + last

			int[] result = new int[length];

			if ((pageSideSteps * 2) + 3 >= pagesCount) // pages to show >= pages
			{
				for (int i = 0; i < pagesCount; i++)
				{
					result[i] = i + 1;
				}
			}
			else // pages number is too much
			{
				result[0] = 1;// add 1st page

				if (currentPage - pageSideSteps <= 1) // index is close to first page
				{
					for (int i = 1; i <= (pageSideSteps * 2)+1; i++)
					{
						result[i] = i + 1;
					}
				}
				else if (currentPage + pageSideSteps >= pagesCount) // index is close to last page
				{
					for (int i = 1; i <= (pageSideSteps * 2)+1; i++)
					{
						result[i] = (i + pagesCount - (pageSideSteps * 2) - 1);
					}
				}
				else // index is in the middle
				{
					for (int i = 1; i <= (pageSideSteps * 2)+1; i++)
					{
						result[i] = (i + currentPage - pageSideSteps - 1);
					}
				}

				result[length - 1] = pagesCount;// add last page
			}

			return result;
		}
		
		public static int GetPageIndexFromArray(int currentPage, int[] paginationArray)
		{
			for (int i = 0; i < paginationArray.Length; i++)// replace with while
			{
				if (currentPage == paginationArray[i])
				{
					return i;
				}
			}

			return -1;
		}
		
	}
}
