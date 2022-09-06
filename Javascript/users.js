const searchBar = document.querySelector(".users .search input"),
    searchBtn = document.querySelector(".users .search button");
   
searchBtn.onclick = () => {
    console.log("sup ii");
    if (!searchBar) {
        console.log("cant find");
    }
    else {
        console.log("fund");
    }
    searchBar.classList.toggle("show");
    searchBar.focus();
    searchBtn.classList.toggle("active");

    
}

