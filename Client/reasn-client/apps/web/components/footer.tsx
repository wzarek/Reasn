import React from "react";

const Footer = () => {
return(
<footer className="text-center py-20 bg-black bg-bottom absolute inset-x-0 bottom-0 rounded-t-2xl">
  <div className="flex items-center justify-between w-full px-80"> 
    <div className="flex-grow h-1 rounded-3xl mx-1 mt-1 bg-gradient-to-r from-red-400 via-purple-500 to-indigo-600">
    </div>
    <span className="text-3xl font-bold text-white">we are</span>
    <div className="flex-grow h-1 rounded-3xl mx-1 mt-1 bg-gradient-to-r from-indigo-600 via-purple-500 to-red-400"></div>
  </div>
  <div className="text-3xl font-bold text-white -mt-2">the Reasn.</div>
  <div className="flex justify-between mx-96 -mt-10">
    <div className="flex gap-16 text-gray-400">
      <a href="#">link</a>
      <a href="#">link</a>
      <a href="#">link</a>
    </div>
    <div className="flex gap-16 text-gray-400">
      <a href="#">link</a>
      <a href="#">link</a>
      <a href="#">link</a>
    </div>
  </div>
</footer>
)
}
export default Footer