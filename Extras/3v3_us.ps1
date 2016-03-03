while(1) { 
   
    Invoke-RestMethod "http://localhost:5001/api/region/us/bracket/3v3?locale=en_us"; 
    echo "done";
    sleep -sec 30; 
     }