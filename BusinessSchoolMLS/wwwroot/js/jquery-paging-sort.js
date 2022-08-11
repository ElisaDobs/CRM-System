/*
        Author            : Tsaleni Dobson Maswanganye
        Date              : 2018 - 06 - 13
*/

var SortPaging = function (tblCtrl) {
    this.tblCtrl = tblCtrl;
};

SortPaging.prototype = {
    loadSortPaging: function () {
        var obj = this;
        obj.loadPagination();
        obj.loadColumnSorting();
    },
    loadPagination: function () {
        var obj = this;
        $(".pagination > li").each(function (index) {
            $(this).bind("click", function () {
                obj.showPage($(this.getElementsByTagName("span")).text());
            });
        });

        obj.showFirstHideRest();
    },
    showFirstHideRest: function () {
        var obj = this;
        obj.tblCtrl.find("tbody > tr").each(function (index) {
            if ($(this).attr("pageIndex") === "1") {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    },
    showPage: function (pageIndex) {
        var obj = this;
        obj.tblCtrl.find("tbody > tr").each(function (index) {
            if ($(this).attr("pageIndex") === pageIndex) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    },
    loadColumnSorting: function () {
        var obj = this;
        obj.tblCtrl.find("thead > tr").each(function (index) {
            $(this.getElementsByTagName("th")).bind("click", function () {
                obj.sortByColumnIndex($(this).attr("columnIndex"));
            });
        });
    },
    sortByColumnIndex: function (columIndex) {

        var obj = this,
            sort_tbl = document.getElementById(obj.tblCtrl.attr("id")).getElementsByTagName("tbody"),
            rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
        dir = "asc";

        switching = true;

        while (switching) {
            switching = false;

            rows = $(sort_tbl).find("tr").each(function (index) { return this; });

            for (i = 1; i < (rows.length - 1); i++) {
                shouldSwitch = false;
                x = rows[i].getElementsByTagName("td")[columIndex];
                y = rows[i + 1].getElementsByTagName("td")[columIndex];
                if (dir === "asc") {
                    if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                        shouldSwitch = true;
                        break;
                    }
                } else if (dir === "desc") {
                    if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                        shouldSwitch = true;
                        break;
                    }
                }
            }
            if (shouldSwitch) {

                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                switching = true;
                switchcount++;
            } else {
                if (switchcount === 0 && dir === "asc") {
                    dir = "desc";
                    switching = true;
                }
            }
        }
    }
};