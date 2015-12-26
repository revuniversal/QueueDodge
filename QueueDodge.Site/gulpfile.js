/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
var gulp = require('gulp');

var mainBowerFiles = require('main-bower-files');
series = require('stream-series'),
inject = require('gulp-inject'),
angularFilesort = require('gulp-angular-filesort'),
concat = require('gulp-concat'),
uglify = require('gulp-uglify'),
sourcemaps = require('gulp-sourcemaps')




gulp.task('BOWER_INJECT', function () {

    var target = gulp.src('./wwwroot/index.html');
    return target
       .pipe(inject(gulp.src(mainBowerFiles(), { read: false }).pipe(concat('vendor.js')), { name: 'bower', relative: true }))
       .pipe(gulp.dest('./wwwroot/'));
});



gulp.task('INJECT', function () {


    var css = gulp.src(['./wwwroot/**/*.css']);

    var appScripts = gulp.src(['./wwwroot/app/**/*.js', '!./wwwroot/vendor/**/*.js', '!./wwwroot/lib/**/*.js', '!./wwwroot/app/app.js']).pipe(angularFilesort());
    var target = gulp.src('./wwwroot/index.html');


    target
      .pipe(
      inject(series(appScripts)
        //.pipe(concat('app2.js'))
        //.pipe(gulp.dest('./wwwroot/'))
        , { relative: true }))
      .pipe(gulp.dest('./wwwroot/'));

    target
  .pipe(
  inject(series(css)
    //.pipe(concat('style.css'))
    //.pipe(gulp.dest('./wwwroot/'))
    , { relative: true }))
  .pipe(gulp.dest('./wwwroot/'));
});
