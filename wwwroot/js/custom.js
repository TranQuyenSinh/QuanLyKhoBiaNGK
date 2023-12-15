function removeDetail(button) {
   $(button).closest('tr').remove()
}

$('.clickable-row').click(function () {
   let url = $(this).data('href')
   if (url) window.location = $(this).data('href')
})
