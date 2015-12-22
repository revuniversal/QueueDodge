/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
var gulp = require('gulp');
var mainBowerFiles = require('gulp-main-bower-files');
var inject = require('gulp-inject');

var paths = {
    webroot: "./wwwroot/"
};

paths.vendorScripts = paths.webroot + "vendor/*.js";
paths.vendorStyles = paths.webroot + "vendor/*.css";
 
gulp.task('index', function () {
    var target = gulp.src('./index.html');
    // It's not necessary to read the files (will speed up things), we're only after their paths: 
    var sources = gulp.src(['./**/*.js', './**/*.css'], { read: false });

    return target.pipe(inject(sources))
      .pipe(gulp.dest('./'));
});

gulp.task('main-bower-files', function() {
    return gulp.src('./bower.json')
        .pipe(mainBowerFiles({
            overrides: {
                bootstrap: {
                    main: [
                        './dist/js/bootstrap.js',
                        './dist/css/*.min.*',
                        './dist/fonts/*.*'
                    ]
                }
            }
        }))
        .pipe(gulp.dest('./wwwroot/vendor'));
});