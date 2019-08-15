$(document).ready(function () {
    var menu = document.querySelector('nav');
    var menuPosition = menu.getBoundingClientRect();
    var placeholder = document.createElement('div');
    placeholder.style.width = menuPosition.width + 'px';
    placeholder.style.height = (menuPosition.height + 20) + 'px';
    var isAdded = false;
    window.addEventListener('scroll', function () {
        if (window.pageYOffset >= menuPosition.top && !isAdded) {
            menu.classList.add('sticky');
            menu.parentNode.insertBefore(placeholder, menu);
            isAdded = true;
        } else if (window.pageYOffset < menuPosition.top && isAdded) {
            menu.classList.remove('sticky');
            menu.parentNode.removeChild(placeholder);
            isAdded = false;
        }
    });
});
$(document).on('change', '.btn-file :file', function() {
  var input = $(this),
      numFiles = input.get(0).files ? input.get(0).files.length : 1,
      label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
  input.trigger('fileselect', [numFiles, label]);
});

$(document).ready( function() {
    $('.btn-file :file').on('fileselect', function(event, numFiles, label) {
        
        var input = $(this).parents('.input-group').find(':text'),
            log = numFiles > 1 ? numFiles + ' files selected' : label;
        
        if( input.length ) {
            input.val(log);
        } else {
            if( log ) alert(log);
        }
        
    });
});