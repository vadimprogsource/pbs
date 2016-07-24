Handlebars.registerHelper('pagination', function (context)
{

    var pageIndex  = context.data.root.PageIndex;
    var totalPages = context.data.root.TotalPages;





    if (totalPages && totalPages > 1) switch (context.hash.type)
    {
        case "prev":
        {
            if (pageIndex < 1)
            {
                return "";
            }

            return context.fn({ pageIndex: --pageIndex });
        }

        case "next":
        {
            if (pageIndex < totalPages - 1)
            {
                return context.fn({ pageIndex: ++pageIndex });
            }

            return "";
          
        }

        default:
        {
            var limit = context.hash.limit;

            if (!limit)
            {
                limit = 10;
            }


            if (totalPages <= limit)
            {
                var html = "";

                for (var i = 0 ; i < totalPages ; i++)
                {
                    html += context.fn({ pageIndex: i, pageNumber: i + 1, isCurrentPage: i == pageIndex });
                }

                return html;
            }

            var html_left = "", html_right = "";

            for (var i = 0, inc = pageIndex, dec = pageIndex  ; i < limit; i++)
            {
                if (--dec >= 0)
                {
                    html_left = context.fn({ pageIndex: dec, pageNumber: dec + 1, isCurrentPage: false }) + html_left;
                }


                if (++inc < totalPages)
                {
                    html_right += context.fn({ pageIndex: inc, pageNumber: inc + 1, isCurrentPage: false });
                }
            }


            if (--dec >= 0)
            {
                html_left = context.fn({ pageIndex: dec, pageNumber: "...", isCurrentPage: false })+html_left;
            }

            if (++inc < totalPages)
           {
               html_right += context.fn({ pageIndex: inc, pageNumber: "...", isCurrentPage: false });
           }
            
           return html_left + context.fn({ pageIndex: pageIndex, pageNumber: pageIndex+1, isCurrentPage: true })+html_right;
        }
    }



});