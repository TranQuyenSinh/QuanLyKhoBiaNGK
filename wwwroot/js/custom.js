function removeDetail(button) {
   $(button).closest('tr').remove()
}

$('.clickable-row').click(function () {
   window.location = $(this).data('href')
})
