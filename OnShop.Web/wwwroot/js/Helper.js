function DeleteSiteItem(url, id, element) {
    Swal.fire({
        title: 'حذف!',
        text: "آیا از حذف اطمینان دارید؟!",
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#28a745',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        reverseButtons: true

    }).then((result) => {
        if (result.isConfirmed) {
            var postData = {
                'id': id
            };
            BlockUI(element);
            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: url,
                data: postData,
                success: function (data) {
                    if (data.IsSuccess == true) {
                        Swal.fire(
                            'موفق!',
                            data.Message,
                            'success'
                        ).then(function (isConfirm) {
                            location.reload();
                        });
                    }
                    else {
                        Swal.fire(
                            'هشدار!',
                            data.Message,
                            'warning'
                        );
                        UnBlockUI(element);
                    }
                },
                error: function (request, status, error) {
                    UnBlockUI(element);
                    alert(request.responseText);
                }

            });

        }
    });
}


function BlockUI(element) {
    $(element).block({
        message: "<div>در حال تبادل اطلاعات</div>",
        css: {
            padding: 0,
            margin: 0,
            width: '30%',
            top: '40%',
            left: '35%',
            textAlign: 'center',
            color: '#000',
            border: '3px solid #aaa',
            backgroundColor: '#fff',
            cursor: 'wait'
        }
    });
}
function UnBlockUI(id) {
    $(element).unblockUI();
}