/**
 * 一些通用方法
 */
(function(exports) {

    /**
     * 将string字符串转为html对象,默认创一个div填充
     * 因为很常用，所以单独提取出来了
     * @param {String} strHtml 目标字符串
     * @return {HTMLElement} 返回处理好后的html对象,如果字符串非法,返回null
     */
    exports.parseHtml = function(strHtml) {
        if (typeof strHtml !== 'string') {
            return strHtml;
        }
        // 创一个灵活的div
        var i,
            a = document.createElement('div');
        var b = document.createDocumentFragment();

        a.innerHTML = strHtml;

        while ((i = a.firstChild)) {
            b.appendChild(i);
        }

        return b;
    };

    /**
     * 将对象渲染到模板
     * @param {String} template 对应的目标
     * @param {Object} obj 目标对象
     * @return {String} 渲染后的模板
     */
    exports.renderTemplate = function(template, obj) {
        return template.replace(/[{]{2}([^}]+)[}]{2}/g, function($0, $1) {
            return obj[$1] || '';
        });
    };

    /**
     * 将对象渲染到模板
     * @param {String} template 对应的目标
     * @param {Object} obj 目标对象
     * @return {String} 渲染后的模板
     */
    exports.prepareData = function(template, obj) {
        return template.replace(/[{]{2}([^}]+)[}]{2}/g, function($0, $1) {
            return obj[$1] || '';
        });
    };

    /**
     * 定义一个计数器
     */
    var counterArr = [0];

    /**
     * 添加测试数据
     * @param {String} dom 目标dom
     * @param {Number} count 需要添加的数量
     * @param {Boolean} isReset 是否需要重置，下拉刷新的时候需要
     * @param {Number} index 属于哪一个刷新
     */
    exports.appendTestData = function(dom, count, isReset, index) {
        if (typeof dom === 'string') {
            dom = document.querySelector(dom);
        }

        var prevTitle = typeof index !== 'undefined' ? ('Tab' + index) : '';

        var counterIndex = index || 0;

        counterArr[counterIndex] = counterArr[counterIndex] || 0;

        if (isReset) {
            dom.innerHTML = '';
            counterArr[counterIndex] = 0;
        }

        var template = '<div id={{commentId}} class="comment"> \
          <div> \
            <div class="author"> \
              <div data-v-f3bf5228="" class="v-tooltip-container" style="z-index: 0;"> \
                <div class="v-tooltip-content"> \
                  <a href="javascript:void(0);" target="_blank" class="avatar"> \
                    <img src="{{commentAvatar}}"> \
                  </a> \
                </div> <!----> \
              </div> \
              <div class="info"> \
                <a href="javascript:void(0);" target="_blank" class="name">{{commentCommenter}} \
                </a> <!----> <!----> \
                <div class="meta"> \
                  <span>{{commentIndex}}楼 · {{commentTime}} \
                  </span> \
                </div> \
              </div> \
            </div> \
            <div class="comment-wrap"> \
              <p>{{commentContent}}</p> \
              <span class="pull-right"> \
                <div class="tool-group" class="pull-right"> \
                  <a href="javascript:void(0)" onclick="addStar(0)"> \
                    <span id="star0" class="glyphicon glyphicon-star-empty" style="color:#969696;top:0px"></span> \
                  </a> \
                  <span id="star_num0" class="label label-default">{{ pid.pid_value | get_star_num(0) }}</span> \
                  <a ng-if="comment.commenter == user.username" class="commentTools comment-delete" href="javascript:void(0);" ng-click="deletefslcmt(saying.id, comment.id)"> \
                    <span>删除</span> \
                  </a> \
                  <a class="commentTools comment-reply" href="javascript:void(0);" ng-click="showSecondComment(comment, comment.commenter)"> \
                    <span>回复</span> \
                  </a> \
                </div> \
              </span> \
            </div> \
          </div> \
        </div> \
        <div class="sub-comment-list">';

        var subCommentTemplate = '<div id={{commentId}} class="sub-comment"> \
              <p> \
                <div data-v-f3bf5228="" class="v-tooltip-container" style="z-index: 0;"> \
                  <div class="v-tooltip-content"> \
                    <a href="javascript:void(0);" target="_blank">{{replier}} \
                    </a>： \
                  </div> <!----> \
                </div>  \
                <span> \
                  <a href="/u/0efd14e6d258" class="maleskine-author" target="_blank" data-user-slug="0efd14e6d258">@{{toCommenter}} \
                  </a> {{replyContent}} \
                </span> \
              </p> \
              <div class="sub-tool-group"> \
                <span>{{replyTime}} \
                </span> \
                <a class=""> \
                  <i class="iconfont ic-comment"> \
                  </i> \
                  <span>回复 \
                  </span> \
                </a> <!----> <!----> \
              </div> \
            </div>';


          var moreCommentTemplate = '<div class="sub-comment more-comment"> \
            <a class="add-comment-btn" href="javascript:void(0);" ng-click="showComment()"> \
              <span>查看全部{{commentChildCommentNumber}}条评论</span> \
              <span class="glyphicon glyphicon-list-alt"></span> \
            </a> <!----> <!----> <!----> \
          </div> <!----> \
          </div>';

        var html = '',
            dateStr = (new Date()).toLocaleString();

        for (var i = 0; i < count; i++) {
            html += exports.renderTemplate(template, {
                commentId: 'comment-' + counterArr[counterIndex],
                commentAvatar: 'https://cdn2.jianshu.io/assets/default_avatar/10-e691107df16746d4a9f3fe9496fd1848.jpg',
                commentCommenter: 'larry',
                commentIndex: counterArr[counterIndex] + 1,
                commentContent: prevTitle + '测试第【' + counterArr[counterIndex] + '】条新闻标题',
                commentStarNumber: 5,
                commentTime: dateStr
            });
            for (var j = 0; j < 3; j++) {
              html += exports.renderTemplate(subCommentTemplate, {
                  commentId: 5,
                  replier: 'klydream',
                  toCommenter: 'larry',
                  replyContent: '这是一条回复',
                  replyTime: dateStr
              });
            }
            html += exports.renderTemplate(moreCommentTemplate, {
                commentChildCommentNumber: 5
            });
            counterArr[counterIndex]++;
        }

        var child = exports.parseHtml(html);

        dom.appendChild(child);
    };

    /**
     * 添加测试数据
     * @param {String} dom 目标dom
     * @param {Number} count 需要添加的数量
     * @param {Boolean} isReset 是否需要重置，下拉刷新的时候需要
     * @param {Number} index 属于哪一个刷新
     */
    exports.appendData = function(dom, count, isReset, index) {
        if (typeof dom === 'string') {
            dom = document.querySelector(dom);
        }

        var prevTitle = typeof index !== 'undefined' ? ('Tab' + index) : '';

        var counterIndex = index || 0;

        counterArr[counterIndex] = counterArr[counterIndex] || 0;

        if (isReset) {
            dom.innerHTML = '';
            counterArr[counterIndex] = 0;
        }

        var template = '<div id={{commentId}} class="comment"> \
          <div> \
            <div class="author"> \
              <div data-v-f3bf5228="" class="v-tooltip-container" style="z-index: 0;"> \
                <div class="v-tooltip-content"> \
                  <a href="javascript:void(0);" target="_blank" class="avatar"> \
                    <img src="{{commentAvatar}}"> \
                  </a> \
                </div> <!----> \
              </div> \
              <div class="info"> \
                <a href="javascript:void(0);" target="_blank" class="name">{{commentCommenter}} \
                </a> <!----> <!----> \
                <div class="meta"> \
                  <span>{{commentIndex}}楼 · {{commentTime}} \
                  </span> \
                </div> \
              </div> \
            </div> \
            <div class="comment-wrap"> \
              <p>{{commentContent}}</p> \
              <span class="pull-right"> \
                <div class="tool-group" class="pull-right"> \
                  <a href="javascript:void(0)" onclick="addStar(0)"> \
                    <span id="star0" class="glyphicon glyphicon-star-empty" style="color:#969696;top:0px"></span> \
                  </a> \
                  <span id="star_num0" class="label label-default">{{ pid.pid_value | get_star_num(0) }}</span> \
                  <a ng-if="comment.commenter == user.username" class="commentTools comment-delete" href="javascript:void(0);" ng-click="deletefslcmt(saying.id, comment.id)"> \
                    <span>删除</span> \
                  </a> \
                  <a class="commentTools comment-reply" href="javascript:void(0);" ng-click="showSecondComment(comment, comment.commenter)"> \
                    <span>回复</span> \
                  </a> \
                </div> \
              </span> \
            </div> \
          </div> \
        </div> \
        <div class="sub-comment-list">';

        var subCommentTemplate = '<div id={{commentId}} class="sub-comment"> \
              <p> \
                <div data-v-f3bf5228="" class="v-tooltip-container" style="z-index: 0;"> \
                  <div class="v-tooltip-content"> \
                    <a href="javascript:void(0);" target="_blank">{{replier}} \
                    </a>： \
                  </div> <!----> \
                </div>  \
                <span> \
                  <a href="/u/0efd14e6d258" class="maleskine-author" target="_blank" data-user-slug="0efd14e6d258">@{{toCommenter}} \
                  </a> {{replyContent}} \
                </span> \
              </p> \
              <div class="sub-tool-group"> \
                <span>{{replyTime}} \
                </span> \
                <a class=""> \
                  <i class="iconfont ic-comment"> \
                  </i> \
                  <span>回复 \
                  </span> \
                </a> <!----> <!----> \
              </div> \
            </div>';


          var moreCommentTemplate = '<div class="sub-comment more-comment"> \
            <a class="add-comment-btn" href="javascript:void(0);" ng-click="showComment()"> \
              <span>查看全部{{commentChildCommentNumber}}条评论</span> \
              <span class="glyphicon glyphicon-list-alt"></span> \
            </a> <!----> <!----> <!----> \
          </div> <!----> \
          </div>';

        var html = '',
            dateStr = (new Date()).toLocaleString();

        for (var i = 0; i < count; i++) {
            html += exports.renderTemplate(template, {
                commentId: 'comment-' + counterArr[counterIndex],
                commentAvatar: 'https://cdn2.jianshu.io/assets/default_avatar/10-e691107df16746d4a9f3fe9496fd1848.jpg',
                commentCommenter: 'larry',
                commentIndex: counterArr[counterIndex] + 1,
                commentContent: prevTitle + '测试第【' + counterArr[counterIndex] + '】条新闻标题',
                commentStarNumber: 5,
                commentTime: dateStr
            });
            for (var j = 0; j < 3; j++) {
              html += exports.renderTemplate(subCommentTemplate, {
                  commentId: 5,
                  replier: 'klydream',
                  toCommenter: 'larry',
                  replyContent: '这是一条回复',
                  replyTime: dateStr
              });
            }
            html += exports.renderTemplate(moreCommentTemplate, {
                commentChildCommentNumber: 5
            });
            counterArr[counterIndex]++;
        }

        var child = exports.parseHtml(html);

        dom.appendChild(child);
    };

})(window.Common = {});
