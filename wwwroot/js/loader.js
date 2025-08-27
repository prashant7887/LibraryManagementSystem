class loader
{
     static showLoader() {
        const loaderWrapper = document.getElementById("loader-wrapper");
        loaderWrapper.classList.remove("hidden");
      }
       static hideLoader() {
        const loaderWrapper = document.getElementById("loader-wrapper");
        loaderWrapper.classList.add("hidden");
      }

}