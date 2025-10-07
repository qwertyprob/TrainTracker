
function showImg(id) {
    const img = document.getElementById(id);
    if (img) {
        img.classList.add('fade-in'); 
        setTimeout(() => {
            img.classList.add('show'); 
        }, 100); 
    }
}