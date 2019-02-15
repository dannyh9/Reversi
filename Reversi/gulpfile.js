const gulp = require('gulp');
const {src, dest} = require('gulp');


gulp.task('read-js-files', function () {
    return src('./js/**/*.js').pipe(dest('dist'));
});

const clean_css = require('gulp-clean-css');
const auto_prefixer = require('gulp-autoprefixer');
const concat = require('gulp-concat');

gulp.task('css', function () {
    return src('./wwwroot/css/**/*.css')
        .pipe(clean_css({ compatibility: 'ie9' }))
        .pipe(auto_prefixer('last 2 version', 'safari 5', 'ie 9'))
        .pipe(concat('style.min.css'))
        .pipe(gulp.dest('./wwwroot/dist'));
});