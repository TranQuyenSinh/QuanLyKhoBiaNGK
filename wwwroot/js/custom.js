function removeDetail(button) {
   $(button).closest('tr').remove()
}

function formatPrice(price) {
   if (price) {
      return Intl.NumberFormat('vi-VN').format(price)
   }
   return 0
}

function randomHexColors(num) {
   var colors = []

   for (var i = 0; i < num; i++) {
      var randomColor = '#' + Math.floor(Math.random() * 16777215).toString(16)
      colors.push(randomColor)
   }

   return colors
}

function createCanvasWithId(canvasId) {
   var canvas = document.createElement('canvas')
   canvas.id = canvasId
   return canvas
}

$('.clickable-row').click(function () {
   let url = $(this).data('href')
   if (url) window.location = $(this).data('href')
})
