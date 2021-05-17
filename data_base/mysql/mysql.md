
# TRIGGER





# Phpmyadmin

How to: Increase phpMyAdmin upload Export / import size
Step 1: Go to php.ini and find the following and change their values to something more higher than your database size. (In this example i have used 20mb).

upload_max_filesize = 20M

post_max_size = 20M

https://community.spiceworks.com/how_to/130036-increase-phpmyadmin-upload-export-import-size


Script timeout passed, if you want to finish import, please resubmit the same file and import will resume
```
Open this configuration file in any editor and change $cfg['ExecTimeLimit'] = 300; to $cfg['ExecTimeLimit'] = 0;
```

https://stackoverflow.com/questions/46175390/script-timeout-passed-if-you-want-to-finish-import-please-resubmit-the-same-fi